using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioTracker : MonoBehaviour
    {
        [SerializeField] private FloatEntityData checkEndingAtSeconds;
        [SerializeField] private FloatEntityData audioPercentLeft;
        [SerializeField] private UnityEvent onAudioEndedPlaying;

        private AudioSource audioSource;
        private float audioLength;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Init()
        {
            audioLength = audioSource.clip.length;
        }

        private void Update()
        {
            if (!audioSource.isPlaying) return;
            audioPercentLeft.Value = 1 - audioSource.time / audioLength;
            if (audioLength - audioSource.time > checkEndingAtSeconds.Value) return;
            onAudioEndedPlaying.Invoke();
        }
    }
}