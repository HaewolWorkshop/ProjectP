using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace HaewolWorkshop
{
    // <T : Enemy<T, TData>, TData : EnemyData>인 이유
    // - FSMPlayer<T>에 전달하기 위함
    // - EnemyData를 필수로 갖도록 하기 위함
    public abstract class Enemy<T, TData> : FSMPlayer<T>, IFSMEntity, ISignalListener where T : Enemy<T, TData> where TData : EnemyData
    {
        public abstract TData Data { get; protected set; }

        // TODO 플레이어를 싱글톤 접근으로 수정
        private Player player;
        public Player Player => player;

        // TODO 플레이어 레이어를 싱글톤 접근으로 수정
        public static int playerLayer = -1;
        public int PlayerLayer => playerLayer;
        
        // Components
        public Rigidbody Rigidbody { get; private set; }
        public NavMeshAgent Agent { get; private set; }

        protected virtual void Awake()
        {
            // TODO 플레이어를 싱글톤 접근으로 수정
            player = FindObjectOfType<Player>();
            if (playerLayer < 0)
            {
                playerLayer = LayerMask.GetMask("Player");
            }

            Rigidbody = GetComponent<Rigidbody>();
            Agent = GetComponent<NavMeshAgent>();

            if (Agent)
            {
                Agent.speed = Data.MoveSpeed;
                Agent.angularSpeed = Data.RotationSpeed;
            }
            // 기본적으로 Rigidbody 자체 연산은 사용하지 않고 NavMeshAgent 만 사용
            Rigidbody.isKinematic = true;
        }

        // 기본적인 움직임: NavMeshAgent
        private Vector3 moveTarget;
        public Vector3 MoveTarget
        {
            get => Agent.destination;
            set
            {
                Agent.destination = value;
                IsStopped = false;
            }
        }

        public bool IsStopped
        {
            get => Agent.isStopped;
            set => Agent.isStopped = value;
        }
        
        // https://answers.unity.com/questions/324589/how-can-i-tell-when-a-navmesh-has-reached-its-dest.html
        public bool IsDestinationReached()
        {
            return !Agent.pathPending
                   && Agent.remainingDistance <= Agent.stoppingDistance
                   && (!Agent.hasPath || Agent.velocity.sqrMagnitude <= 0f);
        }

        // TODO 플레이어 중심 위치 수정 필요
        public Vector3 GetPlayerCenterPosition() => Player.transform.position + Vector3.up * 1f;

        public UnityAction OnDeathEvent { get; set; }

        protected virtual void OnDeath()
        {
            OnDeathEvent?.Invoke();

            if (Agent)
            {
                Agent.enabled = false;
            }
            Rigidbody.isKinematic = false;
        }

        public SignalReactionEvent OnSignalReactionEvent;
        
        // 현재 경계하고 있는 기척
        public SignalReaction currentTrackingSignalReaction = SignalReaction.Invalid;
        public virtual void OnSignal(Signal signal)
        {
            SignalReactionLevel reactionLevel = SignalReactionLevel.Ignore;

            // 인지 범위 1단계 - 시야
            if (IsInSight(signal.position))
            {
                switch (signal.level)
                {
                    // 시야(1)에 보통 기척(2) 발생 시 기척 지점으로 천천히 이동(2)
                    case Signal.NormalLevel:
                        {
                            reactionLevel = SignalReactionLevel.Weak;
                        }
                        break;
                    // 시야(1)에 시끄러운 기척(3) 발생 시 기척 지점으로 빠르게 이동(3)
                    case Signal.LoudLevel:
                        {
                            reactionLevel = SignalReactionLevel.Strong;
                        }
                        break;
                }
            }
            // 인지 범위 2단계 - 근거리
            else if (IsInNearRange(signal.position))
            {
                switch (signal.level)
                {
                    // 근거리(2)에서 보통 기척(2) 발생 시 기척 지점으로 천천히 이동(2)
                    case Signal.NormalLevel:
                        {
                            reactionLevel = SignalReactionLevel.Weak;
                        }
                        break;
                    // 근거리(2)에서 시끄러운 기척(3) 발생 시 기척 지점으로 빠르게 이동(3)
                    case Signal.LoudLevel:
                        {
                            reactionLevel = SignalReactionLevel.Strong;
                        }
                        break;
                }
            }
            // 인지 범위 3단계 - 원거리
            else if (IsInFarRange(signal.position))
            {
                // 원거리(3)에서 시끄러운 기척(3) 발생 시 기척 지점으로 천천히 이동(2)
                if (signal.level == Signal.LoudLevel)
                {
                    reactionLevel = SignalReactionLevel.Weak;
                }
            }

            if (reactionLevel != SignalReactionLevel.Ignore)
            {
                OnSignalReactionEvent.Invoke(new SignalReaction(signal, reactionLevel));
            }
            
        }

        public bool IsPlayerInSight()
        {
            var eyePosition = transform.position; // TODO
            var playerPosition = GetPlayerCenterPosition();
            // 시야각 & 시야범위 체크
            if (!IsInSight(playerPosition))
            {
                return false;
            }

            var toPlayerDirection = playerPosition - eyePosition;
            // 물체 검사
            if (!Physics.Raycast(eyePosition, toPlayerDirection, out var hitInfo, Data.SightRange, Data.SightLayerMask.value))
            {
                return false;
            }
            
            // 플레이어가 아닌 것하고 맞았으면 실패 (장애물에 가림)
            if ((1 << hitInfo.collider.gameObject.layer & PlayerLayer) == 0)
            {
                Debug.DrawLine(eyePosition, hitInfo.point, Color.red);
                return false;
            }
            Debug.DrawLine(eyePosition, hitInfo.point, Color.green);
            
            return true;
        }
        
        public bool IsInSight(Vector3 position)
        {
            var xzForward = transform.forward;
            xzForward.y = 0f; xzForward.Normalize();

            var xzToPosition = position - transform.position;
            xzToPosition.y = 0f;
            var xzDirection = xzToPosition;
            xzDirection.Normalize();

            return Vector3.Dot(xzForward, xzDirection) < Data.SightAngleInCos && xzToPosition.sqrMagnitude <= Data.SightRangeSquared;
        }

        public bool IsInNearRange(Vector3 position) => transform.position.Distance(position) <= Data.NearRange;

        public bool IsInFarRange(Vector3 position) => transform.position.Distance(position) <= Data.FarRange;
        
    }

}