using System.Collections.Generic;
using Mediapipe;
using Mediapipe.Unity;
using UnityEngine;

namespace Hedi.me.BoxWang
{
    public class BodyManager : MonoBehaviour
    {
        [SerializeField] private RectTransform worldAnnotationArea;
        [SerializeField] private Vector3 scale;
        [SerializeField] private JointHandler[] joints;
        [SerializeField] private JointConnectionHandler[] jointConnections;

        private RotationAngle rotationAngle;
        private bool isMirrored;
        private IList<Landmark> landmarks;
        private bool isStale = false;

        private void LateUpdate()
        {
            if (isStale)
                SyncNow();
        }

        public void OnUpdateLandmarks(IList<Landmark> landmarks)
        {
            this.landmarks = landmarks;
            isStale = true;
        }

        public void OnUpdateRotationAngle(RotationAngle rotationAngle, bool isMirrored)
        {
            this.rotationAngle = rotationAngle;
            this.isMirrored = isMirrored;
        }

        protected void SyncNow()
        {
            isStale = false;
            DrawNow(landmarks);
        }

        public void DrawNow(IList<Landmark> landmarks)
        {
            if (landmarks == null)
                return;

            for (int indexJoints = 0; indexJoints < joints.Length; indexJoints++)
            {
                joints[indexJoints].UpdatePosition(worldAnnotationArea.rect, landmarks, scale, rotationAngle, isMirrored);
            }

            for (int indexConnections = 0; indexConnections < jointConnections.Length; indexConnections++)
            {
                jointConnections[indexConnections].Redraw();
            }
        }
    }
}
