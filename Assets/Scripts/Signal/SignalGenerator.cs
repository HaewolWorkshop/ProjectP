using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaewolWorkshop
{
    public class SignalGenerator : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer;

        private float signalRange = 3.0f;

        public void Play(Signal data, float range)
        {
            // Collider Setting.
            Collider[] listenerColliders = Physics.OverlapSphere(data.position, range, targetLayer);

            // listenerColliders is not empty.
            if (listenerColliders.Length > 0)
            {
                for (int count = 0; count < listenerColliders.Length; count++)
                {
                    if (listenerColliders[count].TryGetComponent(out ISignalListener listener))
                    {
                        listener.OnSignal(data);
                    }
                }
            }
#if UNITY_EDITOR
            signalRange = range;
#endif
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            ViewDebugRange();
        }

        private void ViewDebugRange()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, signalRange);
        }
#endif
    }
}