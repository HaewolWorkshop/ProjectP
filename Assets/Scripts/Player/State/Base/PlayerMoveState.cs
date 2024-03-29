using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaewolWorkshop
{
    public class PlayerMoveState : FSMState<Player>
    {
        public PlayerMoveState(IFSMEntity owner) : base(owner)
        {
        }

        public Vector2 moveInput;
        
        public override void InitializeState()
        {
            ownerEntity.OnMove = (x) => moveInput = x;
        }

        public override void FixedUpdateState()
        {
            var velocity = new Vector3(moveInput.x, 0, moveInput.y) * ownerEntity.Status.NormalMoveSpeed;
            
            velocity.y = ownerEntity.rigidbody.velocity.y;
            ownerEntity.rigidbody.velocity = velocity;
        }

        public override void ClearState()
        {
            ownerEntity.OnMove = null;
        }
    }
}