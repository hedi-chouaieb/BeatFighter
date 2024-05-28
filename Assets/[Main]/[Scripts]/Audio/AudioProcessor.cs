using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioProcessor : MonoBehaviour
    {
        [SerializeField] private FloatEntityData bufferDecreaseAmount; // = 0.005f;
        [SerializeField] private FloatEntityData bufferDecreaseMultiplier; // = 1.2f;
        [SerializeField] private FloatEntityData audioProfile; // = 5;

        [SerializeField] private FloatEntityData amplitude;
        [SerializeField] private FloatEntityData amplitudeBuffer;
        [SerializeField] private FloatEntityData amplitudeAverage;
        [SerializeField] private FloatEntityData amplitudeBufferAverage;
        [SerializeField] private FloatEntityData amplitudeHighest;
        [SerializeField] private FloatArrayEntityData audioBand; //new float[8];
        [SerializeField] private FloatArrayEntityData audioBandBuffer; //new float[8];
        [SerializeField] private FloatArrayEntityData freqBand; //new float[8];
        [SerializeField] private FloatArrayEntityData bandBuffer; //new float[8];
        [SerializeField] private FloatArrayEntityData bufferDecrease; //new float[8];
        [SerializeField] private FloatArrayEntityData freqBandHighest; //new float[8];
        [SerializeField] private FloatArrayEntityData samples; //new float[512];

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            audioBand.Value = new float[8];
            audioBand.TriggerOnUpdated();
            audioBandBuffer.Value = new float[8];
            audioBandBuffer.TriggerOnUpdated();
            freqBand.Value = new float[8];
            freqBand.TriggerOnUpdated();
            bandBuffer.Value = new float[8];
            bandBuffer.TriggerOnUpdated();
            bufferDecrease.Value = new float[8];
            bufferDecrease.TriggerOnUpdated();
            freqBandHighest.Value = new float[8];
            freqBandHighest.TriggerOnUpdated();
            samples.Value = new float[512];
            samples.TriggerOnUpdated();
            AudioProfile(audioProfile.Value);
        }

        private void Update()
        {
            if (!_audioSource.isPlaying) return;
            GetSpectrumAudioSource();
            MakeFrequencyBands();
            BandBuffer();
            CreateAudioBands();
            GetAmplitude();
        }

        private void AudioProfile(float audioProfile)
        {
            for (int i = 0; i < 8; i++)
            {
                freqBandHighest.Value[i] = audioProfile;
            }
            freqBandHighest.TriggerOnUpdated();
        }

        private void GetAmplitude()
        {
            float currentAmplitude = 0;
            float currentAmplitudeBuffer = 0;
            for (int i = 0; i < 8; i++)
            {
                currentAmplitude += audioBand.Value[i];
                currentAmplitudeBuffer += audioBandBuffer.Value[i];
            }

            if (currentAmplitude > amplitudeHighest.Value)
            {
                amplitudeHighest.Value = currentAmplitude;
            }

            amplitude.Value = (amplitudeHighest.Value == 0) ? 0 : currentAmplitude / amplitudeHighest.Value;
            amplitudeBuffer.Value = (amplitudeHighest.Value == 0) ? 0 : currentAmplitudeBuffer / amplitudeHighest.Value;
            amplitudeAverage.Value = currentAmplitude / 8.0f;
            amplitudeBufferAverage.Value = currentAmplitudeBuffer / 8.0f;
        }

        private void CreateAudioBands()
        {
            for (int i = 0; i < 8; i++)
            {
                if (freqBand.Value[i] > freqBandHighest.Value[i])
                {
                    freqBandHighest.Value[i] = freqBand.Value[i];
                }
                if (freqBandHighest.Value[i] == 0) continue;
                audioBand.Value[i] = (freqBand.Value[i] / freqBandHighest.Value[i]);
                audioBandBuffer.Value[i] = (bandBuffer.Value[i] / freqBandHighest.Value[i]);
            }
            audioBand.TriggerOnUpdated();
            audioBandBuffer.TriggerOnUpdated();
        }

        private void BandBuffer()
        {
            for (int g = 0; g < 8; ++g)
            {
                if (freqBand.Value[g] > bandBuffer.Value[g])
                {
                    bandBuffer.Value[g] = freqBand.Value[g];
                    bufferDecrease.Value[g] = bufferDecreaseAmount.Value;

                }

                if (freqBand.Value[g] < bandBuffer.Value[g])
                {
                    bandBuffer.Value[g] -= bufferDecrease.Value[g];
                    bufferDecrease.Value[g] *= bufferDecreaseMultiplier.Value;
                }
            }
            bandBuffer.TriggerOnUpdated();
            bufferDecrease.TriggerOnUpdated();
        }

        private void GetSpectrumAudioSource()
        {
            _audioSource.GetSpectrumData(samples.Value, 0, FFTWindow.Blackman);
            samples.TriggerOnUpdated();
        }

        private void MakeFrequencyBands()
        {
            /*
            * 22050 / 512 = 43hertz per sample
            * 
            * 20 - 60 hertz
            * 60 - 250 hertz
            * 250 - 500 hertz
            * 500 - 2000 hertz
            * 2000 - 4000 hertz
            * 4000 - 6000 hertz
            * 6000 - 20000 hertz
            * 
            * 0 - 2   = 86 hertz
            * 1 - 4   = 172 hertz    - 87-258
            * 2 - 8   = 344 hertz    - 259-602
            * 3 - 16  = 688 hertz    - 603-1290
            * 4 - 32  = 1376 hertz   - 1291-2666
            * 5 - 64  = 2752 hertz   - 2667-5418
            * 6 - 128 = 5504 hertz   - 5419-10922
            * 7 - 256 = 11008 hertz  - 10923-21930
            * 510
            */

            int count = 0;

            for (int i = 0; i < 8; i++)
            {
                float average = 0;
                int sampleCount = (int)Mathf.Pow(2, i) * 2;
                if (i == 7)
                {
                    sampleCount += 2;
                }

                for (int j = 0; j < sampleCount; j++)
                {
                    average += samples.Value[count] / _audioSource.volume * (count + 1);
                    count++;
                }

                average /= count;

                freqBand.Value[i] = average * 10;
            }
            freqBand.TriggerOnUpdated();
        }

        [System.Serializable] public class UnityEventFloat : UnityEvent<float> { }
        [System.Serializable] public class UnityEventFloatArray : UnityEvent<float[]> { }
    }
}