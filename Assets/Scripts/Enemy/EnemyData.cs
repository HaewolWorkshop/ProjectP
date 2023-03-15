using System;
using UnityEngine;

namespace HaewolWorkshop
{
    public abstract class EnemyData : ScriptableObject
    {
        [field: Header("기본 스펙")]
        
        [Tooltip("기본 이동 속도")]
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;
        [Tooltip("달리기 속도")]
        [field: SerializeField] public float RunSpeed { get; private set; } = 7f;
        [Tooltip("초당 회전각")]
        [field: SerializeField] public float RotationSpeed { get; private set; } = 10f;
        [Tooltip("활동 반경")]
        [field: SerializeField] public float MoveRadius { get; private set; } = 100f;

        
        
        [field: Header("인지 스펙")]
        
        [Tooltip("플레이어 발견 시 사용될 벽 레이어")]
        [field: SerializeField] public LayerMask SightLayerMask { get; private set; }
        [Tooltip("시야 범위(인지 범위 1단계)")]
        [field: SerializeField] public float SightRange { get; private set; } = 10f;
        [Tooltip("시야각(인지 범위 1단계)")]
        [field: SerializeField] public float SightAngle { get; private set; } = 90f;
        [Tooltip("가까운 거리(인지 범위 2단계)")]
        [field: SerializeField] public float NearRange { get; private set; } = 5f;
        [Tooltip("먼 거리(인지 범위 3단계)")]
        [field: SerializeField] public float FarRange { get; private set; } = 15f;

        public float SightRangeSquared { get; private set; }
        public float SightAngleInCos { get; private set; }
        private void Awake()
        {
            SightRangeSquared = SightRange * SightRange;
            SightAngleInCos = Mathf.Cos(SightAngle * Mathf.Deg2Rad);
        }
    }
}