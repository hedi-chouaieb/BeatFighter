using UnityEngine;

namespace Hedi.me.BoxWang
{
    public class JointConnectionHandler : MonoBehaviour
    {
        [SerializeField] private Transform startTarget;
        [SerializeField] private Transform endTarget;
        [SerializeField] private LineRenderer lineRenderer;

        public void Redraw()
        {
            lineRenderer.SetPositions(new Vector3[] { startTarget.localPosition, endTarget.localPosition });
        }
    }
}
