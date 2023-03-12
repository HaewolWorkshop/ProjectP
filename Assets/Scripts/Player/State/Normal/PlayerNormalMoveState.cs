using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        
    }
}
