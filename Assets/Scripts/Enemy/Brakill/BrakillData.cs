using UnityEngine;

namespace HaewolWorkshop
{
    [CreateAssetMenu(fileName = "Brakill Data", menuName = "Data/Enemy Data/Brakill Data", order = 0)]
    public class BrakillData : EnemyData
    {
        [field: Header("브래킬 설정")]
        [field: SerializeField] public float MinimumDistanceToCeil { get; private set; } = 10f;
        [field: SerializeField] public float CeilAdjustSpeed { get; private set; } = 10f;
        [field: SerializeField] public LayerMask CeilMask { get; private set; }
    }
}