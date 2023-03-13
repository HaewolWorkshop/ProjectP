using UnityEngine;

namespace HaewolWorkshop
{
    public static class VectorExtension
    {
        public static float Distance(in this Vector3 v, in Vector3 other)
        {
            return (v - other).magnitude;
        }
        public static float DistanceSquared(in this Vector3 v, in Vector3 other)
        {
            return (v - other).sqrMagnitude;
        }
    }
}