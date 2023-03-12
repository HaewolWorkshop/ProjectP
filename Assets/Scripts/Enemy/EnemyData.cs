using UnityEngine;

namespace HaewolWorkshop
{
    public abstract class EnemyData : ScriptableObject
    {
        [Tooltip("이동속도")]
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;
        
        [Tooltip("초당 회전각")]
        [field: SerializeField] public float RotationSpeed { get; private set; } = 10f;
    }
}