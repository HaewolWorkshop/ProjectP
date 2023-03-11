using UnityEngine;

namespace HaewolWorkshop
{
    public class Brakill : Enemy<Brakill, BrakillData>, IWanderable
    {
        public enum BrakillStates : int
        {
            Idle,
            Wander,
            
            Max
        }
        [field: SerializeField] public override BrakillData Data { get; protected set; }
        
        [field: SerializeField] public EnemyWanderType WanderType { get; set; }
        [field: SerializeField] public EnemyWanderSpots WanderSpots { get; set; }
        
        protected override void Awake()
        {
            base.Awake();
            
            // SetUp(DummyEnemyState.Idle); // TODO
            SetUp(BrakillStates.Wander);
        }
    }
}