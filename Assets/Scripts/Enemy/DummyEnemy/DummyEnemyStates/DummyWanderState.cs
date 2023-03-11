using UnityEngine;
using UnityEngine.AI;

namespace HaewolWorkshop
{
    [FSMState((int)DummyEnemy.DummyEnemyState.Wander)]
    public class DummyWanderState : FSMState<DummyEnemy>
    {
        private EnemyWanderSpots Spots => ownerEntity.spots;
        private NavMeshAgent Agent => ownerEntity.Agent;

        // https://answers.unity.com/questions/324589/how-can-i-tell-when-a-navmesh-has-reached-its-dest.html
        private bool IsDestinationReached()
        {
            return !Agent.pathPending
                   && Agent.remainingDistance <= Agent.stoppingDistance
                   && (!Agent.hasPath || Agent.velocity.sqrMagnitude <= 0f);
        }
        
        public DummyWanderState(IFSMEntity entity) : base(entity)
        {
        }

        public override void FixedUpdateState()
        {
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
            if (IsDestinationReached() && spotCount > 1)
            {
                switch (ownerEntity.wanderType)
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
    }
}