using System;
using UnityEngine;

namespace HaewolWorkshop
{
    public class DummyEnemy : Enemy<DummyEnemy, DummyEnemyData>
    {
        public enum DummyEnemyState : int
        {
            Idle,
            Wander,
            
            Max
        }
        [field: SerializeField] public override DummyEnemyData Data { get; protected set; }

        public EnemyWanderType wanderType;
        public EnemyWanderSpots spots;
        
        protected override void Awake()
        {
            base.Awake();
            
            // SetUp(DummyEnemyState.Idle); // TODO
            SetUp(DummyEnemyState.Wander);
        }
    }
}