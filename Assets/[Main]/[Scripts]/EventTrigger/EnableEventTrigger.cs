using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    public class EnableEventTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent onEnable;
        private void OnEnable()
        {
            onEnable?.Invoke();
        }
    }
}
