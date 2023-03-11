using System;
using UnityEngine;

namespace HaewolWorkshop
{
    public class DummyEnemy : Enemy<DummyEnemy, DummyEnemyData>
    {
        public enum DummyEnemyState : int
        {
            Idle,
            
            Max
        }
        [field: SerializeField] public override DummyEnemyData Data { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            SetUp(DummyEnemyState.Idle);
        }
    }
}