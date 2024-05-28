using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private UnityEvent onAudioProcessorMusicPlay;
        [SerializeField] private UnityEvent onMusicPlay;
        [SerializeField] private TargetHandler target;
        [SerializeField] private FloatEntityData speed;
        [SerializeField] private Vector3EntityData direction;
        [SerializeField] private bool destroyImmediately;

        public void Play()
        {
            target.gameObject.SetActive(true);
            target.StartMoving(direction.Value, speed.Value, Color.black, null);
            onAudioProcessorMusicPlay?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != target.gameObject) return;

            onMusicPlay?.Invoke();
            target.Destroy(destroyImmediately);
            target.gameObject.SetActive(!destroyImmediately);
        }
    }
}
