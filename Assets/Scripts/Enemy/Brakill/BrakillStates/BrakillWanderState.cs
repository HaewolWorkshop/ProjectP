using UnityEngine;

namespace HaewolWorkshop
{
    [FSMState((int)Brakill.BrakillStates.Wander)]
    public class BrakillWanderState : EnemyWanderStateTemplate<Brakill, BrakillData>
    {
        public BrakillWanderState(IFSMEntity entity) : base(entity)
        {
        }
    }
}