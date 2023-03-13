using System.Collections.Generic;
using UnityEngine;

namespace HaewolWorkshop
{
    public class EnemyWanderSpots : MonoBehaviour
    {
        public List<Transform> spots = new();
        
        [ContextMenu("Bind Children Objects As Spots")]
        private void BindChildren()
        {
            spots.Clear();
            int index = 0;
            foreach (var childTransform in transform.GetComponentsInChildren<Transform>())
            {
                if(childTransform == transform) continue;
                childTransform.gameObject.name = index.ToString("00");
                spots.Add(childTransform);
                ++index;
            }
        }
        
        public int Count => spots.Count;
    
        public Transform this[int index]
        {
            get => spots[index];
            set => spots[index] = value;
        }

        public bool IsNotEmpty() => Count > 0;

#if UNITY_EDITOR
        [SerializeField] private bool drawLines = true;
        private void OnDrawGizmos()
        {
            if (!drawLines)
            {
                return;
            }
            // 스팟 위치 그리기
            Gizmos.color = Color.green;
            var spotCount = spots.Count;
            for (int i = 0; i < spotCount; ++i)
            {
                if(!spots[i]) continue;
                var spotPosition = spots[i].position;
                var next = spots[(i + 1) % spotCount];
                if(!next) continue;
                Gizmos.DrawLine(spotPosition, next.position);
            }
        }
#endif
    }
}