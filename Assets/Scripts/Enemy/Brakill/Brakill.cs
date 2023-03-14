using System;
using UnityEngine;

namespace HaewolWorkshop
{
    public class Brakill : Enemy<Brakill, BrakillData>, IWanderable
    {
        public enum BrakillStates : int
        {
            Idle,
            Wander,
            
            Track,
            Chase,
            
            Max
        }
        [field: SerializeField] public override BrakillData Data { get; protected set; }
        
        [field: SerializeField] public EnemyWanderType WanderType { get; set; }
        [field: SerializeField] public EnemyWanderSpots WanderSpots { get; set; }


        private float initialBaseOffset;
        
        protected override void Awake()
        {
            base.Awake();

            initialBaseOffset = Agent.baseOffset;
            
            // SetUp(DummyEnemyState.Idle); // TODO
            SetUp(BrakillStates.Wander);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            
            AdjustBaseOffsetByCeil();
        }

        // 머리위로 Raycast, 일정 수준 이상 내려오면 BaseOffset 조정
        private void AdjustBaseOffsetByCeil()
        {
            if (initialBaseOffset <= 0f)
            {
                return;
            }

            var position = transform.position;
            float newOffset;
            // 위쪽으로 raycast
            if (Physics.Raycast(position, Vector3.up, out var hitInfo, Data.MinimumDistanceToCeil, Data.CeilMask.value))
            {
                Debug.DrawLine(position, hitInfo.point, Color.yellow);
                float distance = (hitInfo.point - position).magnitude;
                // 최소거리 - 지형에 도달한 거리 == 충돌할 여지가 있는 거리
                // 이를 BaseOffset에서 빼 줌
                newOffset = Mathf.Max(0f,  initialBaseOffset - (Data.MinimumDistanceToCeil - distance));
            }
            else
            {
                Debug.DrawRay(position, Vector3.up * Data.MinimumDistanceToCeil, Color.white);
                // 아니면 기본 base offset으로 설정
                newOffset = initialBaseOffset;
            }

            // 적절하게 보간 (FixedUpdate라 약간 끊겨보임)
            Agent.baseOffset = Mathf.Lerp(Agent.baseOffset, newOffset, Data.CeilAdjustSpeed * Time.deltaTime);
        }

    }
}