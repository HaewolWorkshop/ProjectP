using JetBrains.Annotations;
using UnityEngine;

namespace HaewolWorkshop
{
    /// <summary>
    /// 기척 구조체
    /// </summary>
    public readonly struct Signal
    {
        public readonly int level;
        public readonly Vector3 position;

        public Signal(int level, Vector3 position)
        {
            this.level = level;
            this.position = position;
        }

        public static readonly Signal Invalid = new Signal(-1, Vector3.zero);
        public bool IsInvalid() => level < 0;

        public const int SilentLevel = 0;
        public const int NormalLevel = 1;
        public const int LoudLevel = 2;
    }
}