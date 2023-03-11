using UnityEngine;
using UnityEngine.AI;

namespace HaewolWorkshop
{
    [FSMState((int)DummyEnemy.DummyEnemyState.Wander)]
    public class DummyWanderState : EnemyWanderStateTemplate<DummyEnemy, DummyEnemyData>
    {
        public DummyWanderState(IFSMEntity entity) : base(entity)
        {
        }
    }
}