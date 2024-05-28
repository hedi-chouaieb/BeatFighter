using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    public class StartEventTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent onStart;
        private void Start()
        {
            onStart?.Invoke();
        }
    }
}
