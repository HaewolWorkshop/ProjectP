using UnityEngine;

namespace HaewolWorkshop
{
    [FSMState((int)DummyEnemy.DummyEnemyState.Idle)]
    public class DummyIdleState : FSMState<DummyEnemy>
    {
        public DummyIdleState(IFSMEntity entity) : base(entity)
        {
        }
    }
}