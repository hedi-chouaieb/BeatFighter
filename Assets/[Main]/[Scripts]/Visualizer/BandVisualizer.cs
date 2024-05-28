using UnityEngine;

namespace Hedi.me.BoxWang
{
    public class BandVisualizer : MonoBehaviour
    {
        [SerializeField] int band;
        [SerializeField] private float startScale = 1;
        [SerializeField] private float scaleMultiplier = 10;

        public void VisualizeBand(float[] audioBand)
        {
            transform.localScale = new Vector3(transform.localScale.x, (audioBand[band] * scaleMultiplier) + startScale, transform.localScale.z);
        }

    }
}