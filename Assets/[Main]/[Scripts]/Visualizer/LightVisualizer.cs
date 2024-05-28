using UnityEngine;

namespace Hedi.me.BoxWang
{    public class LightVisualizer : MonoBehaviour
    {
        [SerializeField] int band;
        [SerializeField] private float minIntensity = 0.1f;
        [SerializeField] private float maxIntensity = 1;
        [SerializeField] private ColorEntityData color;
        [SerializeField] private string shaderPropertyName;
        [SerializeField] private Renderer lightVisualizerRenderer;
        [SerializeField] private FloatEntityData bufferDecreaseAmount; // = 0.005f;
        [SerializeField] private FloatEntityData bufferDecreaseMultiplier; // = 1.2f;

        private int shaderPropertyIndex;
        private float intensity = 0;
        private float buffer;
        private float bufferDecrease;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            shaderPropertyIndex = Shader.PropertyToID(shaderPropertyName);
            lightVisualizerRenderer.material.SetColor(shaderPropertyIndex, color.Value * minIntensity);
        }

        public void VisualizeBandWithBuffer(float[] freqBand)
        {
            var audioBand = freqBand[band];

            if (audioBand > buffer)
            {
                buffer = audioBand;
                bufferDecrease = bufferDecreaseAmount.Value;

            }

            if (audioBand < buffer)
            {
                buffer -= bufferDecrease;
                bufferDecrease *= bufferDecreaseMultiplier.Value;
            }

            VisualizeAmplitude(buffer);
        }

        public void VisualizeBand(float[] audioBand)
        {
            VisualizeAmplitude(audioBand[band]);
        }

        public void VisualizeAmplitude(float amplitude)
        {
            intensity = amplitude * (maxIntensity - minIntensity) + minIntensity;
            lightVisualizerRenderer.material.SetColor(shaderPropertyIndex, color.Value * intensity);
        }
    }
}
