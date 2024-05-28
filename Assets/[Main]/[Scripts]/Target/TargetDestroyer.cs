using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    public class TargetDestroyer : MonoBehaviour
    {
        [SerializeField] private bool immediately;
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private UnityEvent onDestroyTarget;

        private void OnCollisionEnter(Collision other)
        {
            var target = other.gameObject.GetComponent<TargetHandler>();
            if (!target) return;

            target.Destroy(immediately);
            scoreManager.TargetDestroyed(target);
            onDestroyTarget?.Invoke();
        }
    }
}
