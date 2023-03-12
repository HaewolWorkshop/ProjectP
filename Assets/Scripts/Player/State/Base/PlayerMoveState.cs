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

        public Vector2 moveInput;
        
        // 여기에서 상속받아서 Normal, Henshin 이동 가능하게 설정하기

        public override void InitializeState()
        {
            ownerEntity.onMove = (x) => moveInput = x;
        }

        public override void UpdateState()
        {
            
        }

        public override void FixedUpdateState()
        {
            // moveSpeed는 Player Data 연계시, 변경 예정
            var velocity = new Vector3(moveInput.x, 0, moveInput.y) * ownerEntity.moveSpeed;
            
            velocity.y = ownerEntity.rigidbody.velocity.y;
            ownerEntity.rigidbody.velocity = velocity;
        }

        public override void ClearState()
        {
            
        }

    }
}