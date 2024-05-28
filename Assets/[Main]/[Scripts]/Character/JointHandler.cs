using System.Collections.Generic;
using Mediapipe;
using Mediapipe.Unity;
using Mediapipe.Unity.CoordinateSystem;
using UnityEngine;

namespace Hedi.me.BoxWang
{
    public class JointHandler : MonoBehaviour
    {
        [SerializeField] private int indexLandmark;
        [SerializeField] private Rigidbody jointRigidbody;
        private Vector3 nextPosition;
        private Vector3 lastPosition;

        public Vector3 NextPosition { get => nextPosition; }
        public Vector3 LastPosition { get => lastPosition; }

        public void UpdatePosition(UnityEngine.Rect annotationArea, IList<Landmark> landmarks, Vector3 scale, RotationAngle rotationAngle, bool isMirrored)
        {
            if (landmarks.Count <= indexLandmark) return;
            nextPosition = annotationArea.GetPoint(landmarks[indexLandmark], scale, rotationAngle, isMirrored);
            if (DistanceSqr(jointRigidbody.position, nextPosition) > Mathf.Epsilon)
                lastPosition = jointRigidbody.position;
            jointRigidbody.MovePosition(nextPosition);
        }

        private float DistanceSqr(Vector3 position1, Vector3 position2)
        {
            return Sqr(position1.x, position2.x) + Sqr(position1.y, position2.y) + Sqr(position1.z, position2.z);
        }

        private float Sqr(float a, float b)
        {
            return Mathf.Pow(a - b, 2);
        }
    }
}
