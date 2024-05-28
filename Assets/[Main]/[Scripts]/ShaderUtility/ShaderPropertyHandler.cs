using UnityEngine;

namespace Hedi.me.BoxWang
{
    public abstract class ShaderPropertyHandler<T> : MonoBehaviour
    {
        [SerializeField] protected Renderer _renderer;
        [SerializeField] protected string propertyName;
        [SerializeField] protected int indexMaterial;
        protected int propertyIndex = -1;

        protected void Awake()
        {
            if (!_renderer) _renderer = this.GetComponent<Renderer>();
            propertyIndex = Shader.PropertyToID(propertyName);
        }

        public void UpdateShaderPropertyValue(T value)
        {
            if (!_renderer)
            {
                Debug.LogWarning($"{nameof(_renderer)} ({_renderer}) is null.", this.gameObject);
                return;
            }
            if (_renderer.materials == null)
            {
                Debug.LogWarning($"{nameof(_renderer.materials)} ({_renderer.materials}) is null.", this.gameObject);
                return;
            }
            if (indexMaterial >= _renderer.materials.Length)
            {
                Debug.LogWarning($"{nameof(indexMaterial)} ({indexMaterial}) Out of Range {nameof(_renderer.materials.Length)} ({_renderer.materials.Length})", this.gameObject);
                return;
            }
            if (propertyIndex < 0) propertyIndex = Shader.PropertyToID(propertyName);
            UpdateMaterialShaderPropertyValue(_renderer.materials[indexMaterial], value);
        }

        protected abstract void UpdateMaterialShaderPropertyValue(Material material, T value);

    }
}
