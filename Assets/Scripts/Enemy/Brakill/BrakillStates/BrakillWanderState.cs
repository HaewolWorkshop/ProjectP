using System;
using UnityEngine;

namespace HaewolWorkshop
{
    [FSMState((int)Brakill.BrakillStates.Wander)]
    public class BrakillWanderState : EnemyWanderStateTemplate<Brakill, BrakillData>
    {
        public BrakillWanderState(IFSMEntity entity) : base(entity)
        {
        }

        protected override ValueType ChaseState => Brakill.BrakillStates.Chase;
        protected override ValueType TrackState => Brakill.BrakillStates.Track;
    }
}