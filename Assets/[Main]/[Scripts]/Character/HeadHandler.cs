using UnityEngine;

namespace Hedi.me.BoxWang
{
    public class HeadHandler : MonoBehaviour
    {
        [SerializeField] private Transform rightEar;
        [SerializeField] private Transform leftEar;
        [SerializeField] private Transform nose;

        private void Update()
        {
            UpdateHeadTransform();
        }

        private void UpdateHeadTransform()
        {
            this.transform.localPosition = (rightEar.position + leftEar.position) / 2.0f;
            this.transform.LookAt(nose);
        }

    }
}
