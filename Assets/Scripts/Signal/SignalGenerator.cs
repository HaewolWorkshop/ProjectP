using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaewolWorkshop
{
    public class SignalGenerator : MonoBehaviour
    {
        private Signal signalData = Signal.Invalid;

        [SerializeField] private float signalRange = 3.0f;

        // Player - Only true, Swing Object - Start on event execution.
        [SerializeField] private bool isPlay = false;
        
        [SerializeField] private LayerMask targetLayer;

        private void Update()
        {
            if (isPlay)
            {
                signalData.position = transform.position;
            }
        }
        private void FixedUpdate()
        {
            if (isPlay)
            {
                // Explore neighborhoods
                SearchRadius();
            }
        }

        private void SearchRadius()
        {
            // Collider Setting.
            Collider[] listenerColliders = Physics.OverlapSphere(transform.position, signalRange, targetLayer);

            // listenerColliders is not empty.
            if (listenerColliders.Length > 0)
            {
                for (int count = 0; count < listenerColliders.Length; count++)
                {
                    if (listenerColliders[count].TryGetComponent(out ISignalListener listener) != null)
                    {
                        listener.OnSignal(signalData);
                    }
                }
            }
        }

        public void SetSignal(float range, int level)
        {
            if (range is < 0 or > 10 || level is < 0 or > 2)
            {
                return;
            }
            signalData.level = level;
            signalRange = range;

            isPlay = true;
        }

        // Use in Close Method 
        public void InitSignalData()
        {
            isPlay = false;
            
            signalData = Signal.Invalid;
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