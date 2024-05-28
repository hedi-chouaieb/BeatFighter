using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    public class TriggerCollisionEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent onTriggerEnter;
        [SerializeField] private UnityEvent onCollisionEnter;

        private void OnTriggerEnter(Collider other)
        {
            onTriggerEnter?.Invoke();
        }

        private void OnCollisionEnter(Collision other)
        {
            onCollisionEnter?.Invoke();
            
        }

    }
}
