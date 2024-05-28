using UnityEngine;

namespace Hedi.me.BoxWang
{
    [RequireComponent(typeof(Rigidbody), typeof(MeshFilter), typeof(Renderer))]
    public class SliceHandler : SpawnableHandler, IMeshFilterRenderer
    {
        [SerializeField] private float explosionForce = 200.0f;

        private bool isCrossSectionMaterialAdded = false;

        public void Init(Color color)
        {
            _rigidbody.velocity = Vector3.zero;
            UpdateColor(color);
        }

        public void AddCrossSectionMaterial(Material crossSectionMat, bool oneTime)
        {
            if (oneTime && isCrossSectionMaterialAdded) return;

            Material[] shared = _renderer.sharedMaterials;

            // otherwise the cross section was added to the back of the submesh array because
            // it uses a different material. We need to take this into account
            Material[] newShared = new Material[shared.Length + 1];

            // copy our material arrays across using native copy (should be faster than loop)
            System.Array.Copy(shared, newShared, shared.Length);
            newShared[shared.Length] = crossSectionMat;
            // the material information
            _renderer.sharedMaterials = newShared;
            isCrossSectionMaterialAdded = true;
        }

        public void Explode(Vector3 direction)
        {
            // sliceRigidbody.AddExplosionForce(explosionForce, transform.position, radius);
            _rigidbody.AddForce(explosionForce * direction, ForceMode.Impulse);
        }

    }
}
