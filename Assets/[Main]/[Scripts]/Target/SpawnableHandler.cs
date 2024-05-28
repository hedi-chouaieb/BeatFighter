using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    [RequireComponent(typeof(Rigidbody), typeof(MeshFilter), typeof(Renderer))]
    public class SpawnableHandler : MonoBehaviour, IMeshFilterRenderer
    {
        [SerializeField] protected Rigidbody _rigidbody;
        [SerializeField] protected MeshFilter _meshFilter;
        [SerializeField] protected Renderer _renderer;
        [SerializeField] protected float secondsToDie = 1.0f;
        [SerializeField] protected UnityEvent onBeforeDie;
        [SerializeField] protected UnityEventFloat onDyingProgress;
        [SerializeField] protected UnityEventSpawnableHandler onDie;
        [SerializeField] protected UnityEventColor onUpdateColor;

        public MeshFilter MeshFilter { get => _meshFilter; }
        public Renderer Renderer { get => _renderer; }
        public Transform Transform { get => transform; }

        protected int shaderPropertyIndex;
        protected int[] shaderColorPropertyIndexes;
        private Color color;

        public UnityEventSpawnableHandler OnDie { get => onDie; }
        public Color Color { get => color; }

        protected virtual void Awake()
        {
            if (!_rigidbody) _rigidbody = this.GetComponent<Rigidbody>();
            if (!_meshFilter) _meshFilter = this.GetComponent<MeshFilter>();
            if (!_renderer) _renderer = this.GetComponent<Renderer>();
        }

        public virtual void Destroy(bool immediately)
        {
            if (immediately)
            {
                onDie?.Invoke(this);
                return;
            }
            onBeforeDie?.Invoke();
            StartCoroutine(Die());
        }

        protected virtual void UpdateColor(Color color)
        {
            this.color = color;
            onUpdateColor?.Invoke(color);
        }

        protected virtual IEnumerator Die()
        {
            float startingTime = Time.time;
            float deltaTime = 0;
            while (deltaTime < secondsToDie)
            {
                yield return null;
                deltaTime = Time.time - startingTime;
                onDyingProgress?.Invoke(deltaTime / secondsToDie);
            }
            onDie?.Invoke(this);
        }

        [System.Serializable] public class UnityEventSpawnableHandler : UnityEvent<SpawnableHandler> { }
        [System.Serializable] public class UnityEventColor : UnityEvent<Color> { }
        [System.Serializable] public class UnityEventFloat : UnityEvent<float> { }

    }
}