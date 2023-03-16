using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HaewolWorkshop
{
    [System.Serializable]
    public struct SignalData
    {
        public float signalRange;
        public int signalLevel;
    }

    [CreateAssetMenu(fileName = "PlayerStatus", menuName = "Data/PlayerStatus", order = 0)]
    public class PlayerStatusData : ScriptableObject
    {
        [field: Header("Share Status")]

        [field: SerializeField] public float Hp { get; private set; }

        [field: SerializeField] public float HenshinGage { get; private set; }

        [field: Header("Normal Status")]

        [field: SerializeField] public float NormalMoveSpeed { get; private set; }
        [field: SerializeField] public float NormalJumpPower { get; private set; }

        [field: SerializeField] public float NormalStealthMoveSpeedMultiplier { get; private set; }

        [field: SerializeField] public float NormalSprintMoveSpeedMultiplier { get; private set; }

        [field: Space(10)] 
        [field: SerializeField] public List<SignalData> NormalSignalDatas { get; private set; }

        [field: Header("Henshin Status")]
        [field: SerializeField] public float HenshinMoveSpeed { get; private set; }
        [field: SerializeField] public float HenshinJumpPower { get; private set; }

        [field: SerializeField] public float HenshinSprintMoveSpeedMultiplier { get; private set; }



    }
}