using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace HaewolWorkshop
{
    public partial class Player : FSMPlayer<Player>, IFSMEntity
    {
        public enum States : int
        {
            Global,
            
            // Normal 
            NormalMove,
            NormalJump,
            NormalSprint,
            NormalStealth,
            NormalClimb
            // ETC
            
            // Henshin 
        }
        
        public Rigidbody rigidbody { get; private set; }
        public Animator animator { get; private set; }

        public float moveSpeed = .0f;

        [Header("Climb")] 
        [SerializeField] public float _wallAngleMax;
        [SerializeField] public float _groundAngleMax;
        [SerializeField] public LayerMask _layerMaskClimb;

        [SerializeField] public float _overpassHeight;
        [SerializeField] public float _stepHeight;

        [SerializeField] public Vector3 _endOffset;
        [SerializeField] public Vector3 _climbOriginDown;

        [SerializeField] public CapsuleCollider collider;

        public Vector3 endPosition;
        public RaycastHit downRaycastHit;
        public RaycastHit forwardRaycastHit;

        public bool _climbing;
        
        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            collider = GetComponent<CapsuleCollider>();
            
            InitInputs();
            
            SetUp(States.NormalMove);
        }

        protected override void Update()
        {
            UpdateInputs();

            base.Update();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        private void OnDrawGizmos()
        {
            if (GetComponent<CapsuleCollider>() == null)
                return;
            
            DrawWorld();
        }

        private void DrawWorld()
        {
            float heightScale = Mathf.Abs(transform.lossyScale.y);
            float radiusScale = Mathf.Max(Mathf.Abs(transform.lossyScale.x), Mathf.Abs(transform.lossyScale.z));

            float radius = collider.radius * radiusScale;
            float totalHeight = Mathf.Max(collider.height * heightScale, radius * 2);

            Vector3 direction = transform.up;
            Vector3 center = transform.TransformPoint(collider.center);
            Vector3 top = center + direction * (totalHeight / 2 - radius);
            Vector3 bottom = center - direction * (totalHeight / 2 - radius);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(top, radius);
            Gizmos.DrawWireSphere(bottom, radius);

        }
    }
}