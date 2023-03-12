using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HaewolWorkshop
{
    [CreateAssetMenu(fileName = "PlayerStatus", menuName = "Status/PlayerStatus", order = 0)]
    public class PlayerStatus : ScriptableObject
    {
        [Header("Gage Status")] 
        
        [SerializeField] private float hp;
        public float Hp => hp;

        [SerializeField] private float henshinPoint;
        public float HenshinPoint => henshinPoint;
        
        [Header("Move Status")]

        [SerializeField] private float moveSpeed;
        public float MoveSpeed => moveSpeed;


        [SerializeField] private float stealthMoveSpeedMultiplier;
        public float StealthMoveSpeedMultiplier => stealthMoveSpeedMultiplier;


        [SerializeField] private float sprintMoveSpeedMultiplier;
        public float SprintMoveSpeedMultiplier => sprintMoveSpeedMultiplier; 
        

        [Header("Jump Status")]

        [SerializeField]
        private float jumpPower;
        public float JumpPower => jumpPower;

    }
}
