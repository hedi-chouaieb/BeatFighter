using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    [RequireComponent(typeof(Rigidbody), typeof(MeshFilter), typeof(Renderer))]
    public class TargetHandler : SpawnableHandler, IMeshFilterRenderer
    {
        [SerializeField] private UnityEventTargetHandler onStartMoving;

        private float speed;
        private Vector3 direction;
        private bool startMoving;
        private StringEntityData orientation;

        public UnityEventTargetHandler OnStartMoving { get => onStartMoving; }
        public StringEntityData Orientation { get => orientation; }

        private void FixedUpdate()
        {
            if (!startMoving) return;

            _rigidbody.velocity = direction * speed * Time.fixedDeltaTime;
        }

        public void StartMoving(Vector3 direction, float speed, Color color, StringEntityData orientation)
        {
            this.direction = direction;
            this.speed = speed;
            this.orientation = orientation;
            UpdateColor(color);
            startMoving = true;
            onStartMoving?.Invoke(this);
        }

        public override void Destroy(bool immediately)
        {
            startMoving = false;
            _rigidbody.velocity = Vector3.zero;
            base.Destroy(immediately);
        }

        [System.Serializable] public class UnityEventTargetHandler : UnityEvent<TargetHandler> { }
    }
}
