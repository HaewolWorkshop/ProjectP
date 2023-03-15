using System;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace HaewolWorkshop
{
    /// <summary>
    /// 특정 지점들을 방황하는 적 상태 스크립트 템플릿, 이 State를 사용하는 적은 IWanderable를 구현해야 함
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public abstract class EnemyWanderStateTemplate<T, TData> : FSMState<T> where T : Enemy<T, TData>, IWanderable where TData : EnemyData
    {
        public EnemyWanderStateTemplate(IFSMEntity entity) : base(entity)
        {
        }

        private EnemyWanderSpots Spots => ownerEntity.WanderSpots;
        private NavMeshAgent Agent => ownerEntity.Agent;

        public override void InitializeState()
        {
            base.InitializeState();

            ownerEntity.OnSignalReactionEvent += OnSignal;
        }

        protected abstract ValueType ChaseState { get; }
        public override void FixedUpdateState()
        {
            // 시야 내 플레이어 존재 시 추적 시작
            if (ownerEntity.IsPlayerInSight())
            {
                ownerEntity.ChangeState(ChaseState);
                return;
            }
            
            // 방황 스팟 없으면 Idle 때 아무것도 안 함
            if (!Spots || !Spots.IsNotEmpty())
            {
                return;
            }

            var currentPosition = ownerEntity.transform.position;
            var spotCount = Spots.Count;
            // 현재 지정된 목표 스팟이 없는 경우 가장 가까운 거 하나 찾아줌
            if (currentWanderingTargetSpot < 0)
            {
                var nearestIndex = 0;
                var nearestDistance = currentPosition.DistanceSquared(Spots[0].position);
                for (int i = 1; i < spotCount; ++i)
                {
                    var distance = currentPosition.DistanceSquared(Spots[i].position);
                    if (nearestDistance > distance)
                    {
                        nearestIndex = i;
                        nearestDistance = distance;
                    }
                }

                SetTargetSpot(nearestIndex);
            }
            // 현재 목표 스팟에 도달한 경우 && 스팟 갯수가 2개 이상인 경우
            if (ownerEntity.IsDestinationReached() && spotCount > 1)
            {
                switch (ownerEntity.WanderType)
                {
                    // 지정된 경로를 계속 순환함
                    case EnemyWanderType.CycleByOrder:
                        {
                            SetTargetSpot((currentWanderingTargetSpot + 1) % spotCount);
                        }
                        break;
                    case EnemyWanderType.Random:
                        {
                            // 랜덤으로 돌림
                            SetTargetSpot(Random.Range(0, spotCount));
                        }
                        break;
                }
            }
        }

        public override void ClearState()
        {
            base.ClearState();
            
            ownerEntity.OnSignalReactionEvent -= OnSignal;
        }

        private int currentWanderingTargetSpot = -1;
        private void SetTargetSpot(int index)
        {
            if (!Spots 
                || !Spots.IsNotEmpty() 
                || index < 0 || index >= Spots.Count
               )
            {
                return;   
            }
            currentWanderingTargetSpot = index;
            ownerEntity.MoveTarget = Spots[index].position;
        }

        protected abstract ValueType TrackState { get; }
        // 현재 상태에서 반응이 있었다면
        protected virtual void OnSignal(SignalReaction reaction)
        {
            if (reaction.IsInvalid())
            {
                return;
            }
            // 갱신이 가능한 기척이라면
            ownerEntity.currentTrackingSignalReaction = reaction;
            ownerEntity.ChangeState(TrackState);
        }
    }
}