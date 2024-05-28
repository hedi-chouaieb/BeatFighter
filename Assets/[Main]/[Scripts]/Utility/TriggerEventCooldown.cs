using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    public class TriggerEventCooldown : MonoBehaviour
    {
        [SerializeField] private bool triggerOnStart;
        [SerializeField] private float cooldownOnSeconds;
        [SerializeField] private UnityEvent onTrigger;

        private void Start()
        {
            if (triggerOnStart)
                TriggerEvent();
        }
        public void TriggerEvent()
        {
            StartCoroutine(TriggerCoroutine());
        }

        private IEnumerator TriggerCoroutine()
        {
            yield return new WaitForSeconds(cooldownOnSeconds);
            onTrigger?.Invoke();
        }
    }
}
