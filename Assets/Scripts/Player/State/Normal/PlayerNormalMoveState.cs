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

        
        // Key 등록은 여기서 -> Normal 에서 할 수 있는 행동
        
        public override void InitializeState()
        {
            base.InitializeState();
            
            ownerEntity.SetAction(Player.ButtonActions.Jump, OnClimb);
            
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
            
            // 테스트용이라 Jump에 묶어둠
            ownerEntity.ClearAction((Player.ButtonActions.Jump));
        }

        private void OnClimb(bool isOn)
        {
            if (isOn)
            {
                ownerEntity.ChangeState(Player.States.NormalClimb);
            }
        }
    }
}
