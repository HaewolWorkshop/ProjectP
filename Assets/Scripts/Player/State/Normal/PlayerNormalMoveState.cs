using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaewolWorkshop
{
    [FSMState((int) Player.States.NormalMove)]
    public class PlayerNormalMoveState : PlayerMoveState
    {
        public PlayerNormalMoveState(IFSMEntity owner) : base(owner)
        {
        }

        private readonly int animXParam = Animator.StringToHash("InputXParam");
        private readonly int animZParam = Animator.StringToHash("InputZParam");

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();

            ownerEntity.animator.SetFloat(animXParam, moveInput.x);
            ownerEntity.animator.SetFloat(animZParam, moveInput.y);
        }
    }
}
