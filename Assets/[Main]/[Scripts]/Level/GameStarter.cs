using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private float secondsToStart = 2.0f;
        [SerializeField] private UnityEventFloat onStartProgress;
        [SerializeField] private UnityEvent onFinished;

        private float progress = 0;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            progress = 0;
        }

        private void OnCollisionStay(Collision other)
        {
            var weapon = other.gameObject.GetComponent<WeaponHandler>();
            if (!weapon) return;

            progress += Time.deltaTime;
            onStartProgress?.Invoke(progress / secondsToStart);

            if (progress >= secondsToStart)
                onFinished?.Invoke();
        }

        private void OnCollisionExit(Collision other)
        {
            var weapon = other.gameObject.GetComponent<WeaponHandler>();
            if (!weapon) return;

            progress = 0;
            onStartProgress?.Invoke(progress);
        }

        [System.Serializable] public class UnityEventFloat : UnityEvent<float> { }
    }
}
