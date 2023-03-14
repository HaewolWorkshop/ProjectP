using JetBrains.Annotations;
using UnityEngine;

namespace HaewolWorkshop
{
    /// <summary>
    /// 기척 구조체
    /// </summary>
    public readonly struct Signal
    {
        /// <summary>
        /// 기척을 발생시킨 원천지
        /// </summary>
        public readonly GameObject source;
        public readonly int level;
        public readonly Vector3 position;

        public Signal(GameObject source, int level, Vector3 position)
        {
            this.source = source;
            this.level = level;
            this.position = position;
        }

        public static readonly Signal Invalid = new Signal(null, -1, Vector3.zero);
        public bool IsInvalid() => source == null || level < 0;

        public const int SilentLevel = 0;
        public const int NormalLevel = 1;
        public const int LoudLevel = 2;
    }
}