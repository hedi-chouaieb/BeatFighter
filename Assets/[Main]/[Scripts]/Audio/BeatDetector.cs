using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    public class BeatDetector : MonoBehaviour
    {
        [SerializeField] private float threshold;
        [SerializeField] private float percent;
        [SerializeField] private UnityEvent onBeat;

        private bool isLastTriggered = false;
        private bool isTrigger = false;
        private float lastAmplitude;

        //AudioPeer.AmplitudeBufferAverage
        public void DetectBeat(float amplitude)
        {
            isTrigger = amplitude > (lastAmplitude * (1 + percent)) && amplitude > threshold;
            lastAmplitude = amplitude;
            if (isTrigger && !isLastTriggered) onBeat?.Invoke();
            isLastTriggered = isTrigger;
        }

    }
}
