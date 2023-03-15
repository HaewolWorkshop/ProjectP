using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace HaewolWorkshop
{
    [FSMState((int)Player.States.NormalClimb)]
    public class PlayerNormalClimbState : FSMState<Player>
    {
        #region OwnerInterface

        public PlayerNormalClimbState(IFSMEntity owner) : base(owner)
        {
        }

        #endregion

        public override void InitializeState()
        {
            ownerEntity.SetAction(Player.ButtonActions.Attack, OnMove);
            

        }

        public override void UpdateState()
        {
            if (CanClimb(out ownerEntity.downRaycastHit, out ownerEntity.forwardRaycastHit,
                    out ownerEntity.endPosition))
            {
                InitiateClimb();
            }
        }

        private void OnMove(bool isOn)
        {
            if (isOn)
            {
                ownerEntity.RevertToPreviousState();
            }
        }

        private bool CanClimb(out RaycastHit downRaycastHitParam, out RaycastHit forwardRaycastHitParam, out Vector3 endPosParam)
        {
            endPosParam = Vector3.zero;
            downRaycastHitParam = new RaycastHit();
            forwardRaycastHitParam = new RaycastHit();
            
            
            bool downHit;
            bool forwardHit;
            bool overpassHit;
            float climbHeight;
            float groundAngle;
            float wallAngle;

            
            RaycastHit overpassRaycastHit;

            Vector3 endPos;
            Vector3 forwardDirectionXZ;
            Vector3 forwardNormalXZ;

            Vector3 downDirection = Vector3.down;
            Vector3 downOrigin = ownerEntity.transform.TransformPoint(ownerEntity._climbOriginDown);

            downHit = Physics.Raycast(downOrigin, downDirection, out ownerEntity.downRaycastHit,
                ownerEntity._climbOriginDown.y - ownerEntity._stepHeight,
                ownerEntity._layerMaskClimb);

            if (downHit)
            {
                float forwardDistance = ownerEntity._climbOriginDown.z;
                Vector3 forwardOrigin = new Vector3(ownerEntity.transform.position.x, ownerEntity.downRaycastHit.point.y - 0.1f,
                    ownerEntity.transform.position.z);
                Vector3 overpassOrigin = new Vector3(ownerEntity.transform.position.x, ownerEntity._overpassHeight,
                    ownerEntity.transform.position.z);

                forwardDirectionXZ = Vector3.ProjectOnPlane(ownerEntity.transform.forward, Vector3.up);
                forwardHit = Physics.Raycast(forwardOrigin, forwardDirectionXZ, out ownerEntity.forwardRaycastHit, forwardDistance,
                    ownerEntity._layerMaskClimb);
                overpassHit = Physics.Raycast(overpassOrigin, forwardDirectionXZ, out overpassRaycastHit,
                    forwardDistance, ownerEntity._layerMaskClimb);
                climbHeight = ownerEntity.downRaycastHit.point.y - ownerEntity.transform.position.y;
                
                
                if (forwardHit)
                {
                    
                    if (overpassHit || climbHeight < ownerEntity._overpassHeight)
                    {

                        forwardNormalXZ = Vector3.ProjectOnPlane(ownerEntity.forwardRaycastHit.normal, Vector3.up);
                        groundAngle = Vector3.Angle(ownerEntity.downRaycastHit.normal, Vector3.up);
                        wallAngle = Vector3.Angle(-forwardNormalXZ, forwardDirectionXZ);

                        if (wallAngle <= ownerEntity._wallAngleMax)
                        {
                            Vector3 vectSurface = Vector3.ProjectOnPlane(forwardDirectionXZ, ownerEntity.downRaycastHit.normal);
                            endPos = ownerEntity.downRaycastHit.point + Quaternion.LookRotation(vectSurface, Vector3.up) *
                                ownerEntity._endOffset;

                            Collider colliderB = ownerEntity.downRaycastHit.collider;
                            bool penetrationOverlap = Physics.ComputePenetration(
                                colliderA: ownerEntity.collider,
                                positionA: endPos,
                                rotationA: ownerEntity.transform.rotation,
                                colliderB: colliderB,
                                positionB: colliderB.transform.position,
                                rotationB: colliderB.transform.rotation,
                                direction: out Vector3 penetrationDirection,
                                distance: out float penetrationDistance
                            );
                            if (penetrationOverlap)
                            {
                                endPos += penetrationDirection * penetrationDistance;
                            }

                            float inflate = -0.05f;
                            float upsweepDistnace = ownerEntity.downRaycastHit.point.y - ownerEntity.transform.position.y;
                            Vector3 upSweepDirection = ownerEntity.transform.up;
                            Vector3 upSweepOrigin = ownerEntity.transform.position;
                            
                            bool upSweepHit = CharacterSweep(
                                position: upSweepOrigin,
                                rotation: ownerEntity.transform.rotation,
                                dir : upSweepDirection,
                                distance: upsweepDistnace,
                                layerMask: ownerEntity._layerMaskClimb,
                                inflate: inflate
                            );

                            Vector3 forwardSweepOrigin =
                                ownerEntity.transform.position + upSweepDirection * upsweepDistnace;
                            Vector3 forwardSweepVector = endPos - forwardSweepOrigin;
                            
                            bool forwardSweepHit = CharacterSweep(
                                position: forwardSweepOrigin,
                                rotation: ownerEntity.transform.rotation,
                                dir: forwardSweepVector.normalized,
                                distance: forwardSweepVector.magnitude,
                                layerMask: ownerEntity._layerMaskClimb,
                                inflate: inflate
                            );
                            
                            if (!upSweepHit && forwardSweepHit)
                            {
                                endPosParam = endPos;
                                downRaycastHitParam = ownerEntity.downRaycastHit;
                                forwardRaycastHitParam = ownerEntity.forwardRaycastHit;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool CharacterSweep(Vector3 position, Quaternion rotation, Vector3 dir,
            float distance, LayerMask layerMask, float inflate)
        {
            float heightScale = Mathf.Abs(ownerEntity.transform.lossyScale.y);
            float radiusScale = Mathf.Max(Mathf.Abs(ownerEntity.transform.lossyScale.x), Mathf.Abs(ownerEntity.transform.lossyScale.z));

            float radius = ownerEntity.collider.radius * radiusScale;
            float totalHeight = Mathf.Max(ownerEntity.collider.height * heightScale, radius * 2);

            Vector3 capsuleUp = rotation * Vector3.up;
            Vector3 center = position + rotation * ownerEntity.collider.center;
            Vector3 top = center + capsuleUp * (totalHeight / 2 - radius);
            Vector3 bottom = center - capsuleUp * (totalHeight / 2 - radius);

            bool sweepHit = Physics.CapsuleCast(
                point1: bottom,
                point2: top,
                radius: radius,
                direction: dir,
                maxDistance: distance,
                layerMask: layerMask
            );
            
            return sweepHit;
        }

        private void InitiateClimb()
        {
            ownerEntity.animator.SetTrigger("Climb");
        }
        
    }
}