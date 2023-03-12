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
        
        // Key 등록은 여기서 -> Normal 에서 할 수 있는 행동
        
        public override void InitializeState()
        {
            base.InitializeState();
            
        }

        public override void UpdateState()
        {
            base.UpdateState();
            
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            
        }

        public override void ClearState()
        {
            base.ClearState();
            
        }
        
    }
}
