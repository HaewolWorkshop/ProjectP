using System;
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
            NormalStealth,
            // ETC
            
            // Henshin 
        }
        
        public Rigidbody rigidbody { get; private set; }
        public Animator animator { get; private set; }

        [SerializeField] private PlayerStatusData status;

        public float moveSpeed = .0f;

        private void Awake()
        {
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

        public PlayerStatusData GetStatus()
        {
            return status;
        }
     
    }
}