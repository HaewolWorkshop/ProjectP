using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

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
     
    }
}