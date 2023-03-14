using System;
using UnityEngine;

namespace HaewolWorkshop
{
    public class DummyEnemy : Enemy<DummyEnemy, DummyEnemyData>, IWanderable
    {
        public enum DummyEnemyState : int
        {
            Idle,
            Wander,
            
            Track,
            Chase,
            
            Max
        }
        [field: SerializeField] public override DummyEnemyData Data { get; protected set; }

        [field: SerializeField] public EnemyWanderType WanderType { get; set; }
        [field: SerializeField] public EnemyWanderSpots WanderSpots { get; set; }

        protected override void Awake()
        {
            base.Awake();
            
            // SetUp(DummyEnemyState.Idle); // TODO
            SetUp(DummyEnemyState.Wander);
        }
    }
}