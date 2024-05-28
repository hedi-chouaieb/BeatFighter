using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private StringEntityData orientation;
        [SerializeField] private SlicerManager slicerManager;
        [SerializeField] private JointHandler jointHandler;
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private UnityEvent onDestroyTarget;

        public StringEntityData Orientation { get => orientation; }

        private void OnCollisionEnter(Collision other)
        {
            var target = other.gameObject.GetComponent<TargetHandler>();
            if (!target) return;
            target.Destroy(false);
            var contactPoint = other.GetContact(0).point;
            slicerManager.Slice(target, contactPoint, Vector2.Perpendicular((Vector2)jointHandler.NextPosition - (Vector2)jointHandler.LastPosition).normalized);
            scoreManager.TargetSliced(this, target, contactPoint);
            onDestroyTarget?.Invoke();
        }

    }
}
