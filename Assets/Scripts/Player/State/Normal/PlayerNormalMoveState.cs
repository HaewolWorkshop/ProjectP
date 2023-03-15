using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HaewolWorkshop
{
    [FSMState((int) Player.States.NormalMove)]
    public class PlayerNormalMoveState : PlayerMoveState
    {
        #region OwnerInterface

        public PlayerNormalMoveState(IFSMEntity owner) : base(owner)
        {
        }

        #endregion

        private readonly int animXParam = Animator.StringToHash("InputXParam");
        private readonly int animZParam = Animator.StringToHash("InputZParam");

        public override void InitializeState()
        {
            base.InitializeState();
            
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            
            ownerEntity.animator.SetFloat(animXParam, Mathf.Round(moveInput.x * 100f) / 100f);
            ownerEntity.animator.SetFloat(animZParam, Mathf.Round(moveInput.y * 100f) / 100f);
        }

        public override void ClearState()
        {
            base.ClearState();
        }

    }
}
