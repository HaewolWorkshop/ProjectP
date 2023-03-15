using UnityEngine;

namespace HaewolWorkshop
{
    [FSMState((int)Brakill.BrakillStates.Idle)]
    public class BrakillIdleState : FSMState<Brakill>
    {
        public BrakillIdleState(IFSMEntity entity) : base(entity)
        {
        }
    }
}