using UnityEngine;

namespace HaewolWorkshop
{
    public abstract class EnemyData : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 10f;
    }
}