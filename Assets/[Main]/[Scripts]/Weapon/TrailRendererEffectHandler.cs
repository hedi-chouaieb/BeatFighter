using UnityEngine;

namespace Hedi.me.BoxWang
{

    public class TrailRendererEffectHandler : MonoBehaviour
    {
        [SerializeField] private TrailRenderer trailRenderer;
        [SerializeField] private Gradient gradientColor;

        public void UpdateColor(Color color)
        {
            for (int i = 0; i < gradientColor.colorKeys.Length; i++)
            {
                gradientColor.colorKeys[i].color = color;
            }
            trailRenderer.colorGradient = gradientColor;
        }
    }
}
