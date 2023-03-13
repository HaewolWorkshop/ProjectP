using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace HaewolWorkshop
{
    // <T : Enemy<T, TData>, TData : EnemyData>인 이유
    // - FSMPlayer<T>에 전달하기 위함
    // - EnemyData를 필수로 갖도록 하기 위함
    public abstract class Enemy<T, TData> : FSMPlayer<T>, IFSMEntity where T : Enemy<T, TData> where TData : EnemyData
    {
        public abstract TData Data { get; protected set; }

        // TODO 플레이어를 싱글톤 접근으로 수정
        private Player player;
        public Player Player => player;
        
        // Components
        public Rigidbody Rigidbody { get; private set; }
        public NavMeshAgent Agent { get; private set; }

        protected virtual void Awake()
        {
            // TODO 플레이어를 싱글톤 접근으로 수정
            player = FindObjectOfType<Player>();

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

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

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
    }
}