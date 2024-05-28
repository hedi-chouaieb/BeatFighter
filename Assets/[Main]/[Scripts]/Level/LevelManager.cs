using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private FloatEntityData checkEndingAtSeconds;
        [SerializeField] private UnityEvent onEndingEnter;
        [SerializeField] private UnityEventFloat onEndingProgress;
        [SerializeField] private UnityEvent onEndingEnd;

        public void EndTheGame()
        {
            onEndingEnter?.Invoke();
            StartCoroutine(EndingCoroutine());
        }

        private IEnumerator EndingCoroutine()
        {
            float startingTime = Time.time;
            float deltaTime = 0;
            while (deltaTime < checkEndingAtSeconds.Value)
            {
                yield return null;
                deltaTime = Time.time - startingTime;
                onEndingProgress?.Invoke(deltaTime / checkEndingAtSeconds.Value);
            }
            onEndingEnd?.Invoke();
        }

        [System.Serializable] public class UnityEventFloat : UnityEvent<float> { }
    }
}