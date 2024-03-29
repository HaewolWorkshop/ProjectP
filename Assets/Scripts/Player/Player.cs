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
        
        public SignalGenerator signalGenerator { get; private set; }

        [field:SerializeField] public PlayerStatusData Status { get; private set; }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            signalGenerator = GetComponent<SignalGenerator>();
            
            InitInputs();
            
            SetUp(States.NormalMove);
        }

        protected override void Update()
        {
            UpdateInputs();

            base.Update();
        }

    }
}