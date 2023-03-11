using System;
using UnityEngine;

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
        public Rigidbody Rigidbody { get; private set; }

        protected virtual void Awake()
        {
            // TODO 플레이어를 싱글톤 접근으로 수정
            player = FindObjectOfType<Player>();

            Rigidbody = GetComponent<Rigidbody>();
        }


        // 기본적인 움직임 로직: 
        private Vector3 moveTarget;
        private bool doMove = false;
        public Vector3 MoveTarget
        {
            get => moveTarget;
            set
            {
                moveTarget = value;
                doMove = true;
            }
        }
        public void StopMove() => doMove = false;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            
            MoveToTarget();
        }

        protected virtual void MoveToTarget()
        {
            var t = transform;
            var currentPosition = t.position;
            var targetPosition = moveTarget;

            var toTarget = targetPosition - currentPosition;
            var isMoving = toTarget.sqrMagnitude > 0f && doMove;

            if (!isMoving)
            {
                return;
            }

            var direction = toTarget;
            direction.y = 0f; direction.Normalize();

            var dt = Time.deltaTime;
            Rigidbody.MovePosition(currentPosition + direction * (Data.MoveSpeed * dt));
            Rigidbody.MoveRotation(Quaternion.Slerp(Rigidbody.rotation, Quaternion.LookRotation(direction), Data.RotationSpeed * dt));
        }
    }
}