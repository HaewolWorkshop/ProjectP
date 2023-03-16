using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaewolWorkshop
{
    [CreateAssetMenu(fileName = "PlayerStatus", menuName = "Data/PlayerStatus", order = 0)]
    public class PlayerStatusData : ScriptableObject
    {
        [field: Header("Share Status")]
        [field: SerializeField]
        public float Hp { get; private set; }

        [field: SerializeField] public float HenshinGage { get; private set; }

        [field: Header("Normal Status")]
        [field: SerializeField] public float NormalMoveSpeed { get; private set; }

        [field: SerializeField] public float NormalJumpPower { get; private set; }

        [field: SerializeField] public float NormalStealthMoveSpeedMultiplier { get; private set; }

        [field: SerializeField] public float NormalSprintMoveSpeedMultiplier { get; private set; }

        [field: Space(10)] [field: Header("Normal Signal Datas")] 
        [field: SerializeField] public float NormalMoveSignalRange;
        [field: SerializeField] public int NormalMoveSignalLevel;


        [field: Header("Henshin Status")]
        [field: SerializeField] public float HenshinMoveSpeed { get; private set; }

        [field: SerializeField] public float HenshinJumpPower { get; private set; }

        [field: SerializeField] public float HenshinSprintMoveSpeedMultiplier { get; private set; }
    }
}