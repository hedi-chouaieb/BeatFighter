using UnityEngine;
using UnityEngine.VFX;

namespace Hedi.me.BoxWang
{
    public abstract class VFXPropertyHandler<T> : MonoBehaviour
    {
        [SerializeField] protected VisualEffect visualEffect;
        [SerializeField] protected string propertyName;
        protected int propertyIndex;

        protected void Awake()
        {
            if (!visualEffect) visualEffect = this.GetComponent<VisualEffect>();
        }

        public void UpdateVFXPropertyValue(T value)
        {
            if (!visualEffect)
            {
                Debug.LogWarning($"{nameof(visualEffect)} ({visualEffect}) is null.", this.gameObject);
                return;
            }
            if (!HasProperty())
            {
                Debug.LogWarning($"{nameof(visualEffect)} ({visualEffect}) has not {nameof(propertyName)} ({propertyName}).", this.gameObject);
                return;
            }
            UpdatePropertyValue(value);
        }

        protected abstract bool HasProperty();
        protected abstract void UpdatePropertyValue(T value);

    }
}
