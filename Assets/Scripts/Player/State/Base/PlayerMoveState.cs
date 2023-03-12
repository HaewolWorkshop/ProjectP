using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaewolWorkshop
{
    public class PlayerMoveState : FSMState<Player>
    {
        #region OwnerInterface

        public PlayerMoveState(IFSMEntity owner) : base(owner)
        {
        }

        #endregion

    }
}