using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaewolWorkshop
{
    public partial class Player : FSMPlayer<Player>, IFSMEntity
    {
        public enum States : int
        {
            Global,
            
            // Normal 
            NormalMove,
            NormalJump,
            NormalSprint,
            NormalStealth
            
            // ETC
            
            // Henshin 
        }
        
        public Rigidbody rigidbody { get; private set; }
        public Animator animator { get; private set; }


        private void Awake()
        {
            if (playerData == null || playerData.Length < 1)
            {
                Debug.LogError($"Player : {playerData.GetType()}이 존재하지 않습니다.");
            }

            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            
            InitInputs();
            
            SetUp(States.NormalMove);
        }

        protected override void Update()
        {
            UpdateInputs();

            base.Update();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}