using EzySlice;
using UnityEngine;

namespace Hedi.me.BoxWang
{

    public class SlicerManager : MonoBehaviour
    {
        [SerializeField] private SliceHandler slicePrefab;
        [SerializeField] private Transform slicesParent;
        [SerializeField] private Material crossSectionMaterial;
        [SerializeField] private float destroyAfterSeconds = 1.0f;

        private GameObject slicedPool;
        private PoolManager<SliceHandler> slicesPool;

        public void Slice(TargetHandler toSlice, Vector3 contactPoint, Vector3 direction)
        {
            SlicedHull slicedObject = toSlice.gameObject.Slice(contactPoint, direction, crossSectionMaterial);
            if (slicedObject == null)
            {
                // Debug.Log("<color=orange>slicedObject == null</color>");
                slicedObject = toSlice.gameObject.Slice(toSlice.transform.position, toSlice.transform.up, crossSectionMaterial);
            }
            CreateSlice(toSlice, slicedObject, true, direction);
            CreateSlice(toSlice, slicedObject, false, -direction);
        }

        private void CreateSlice(TargetHandler toSlice, SlicedHull slicedObject, bool isUpper, Vector3 direction)
        {
            if (slicedObject == null) return;
            if (slicesPool == null)
            {
                slicesPool = new PoolManager<SliceHandler>(slicePrefab, slicesParent);
            }

            var instance = slicesPool.GetInstance();
            instance.Init(toSlice.Color);
            var hullGameObject = isUpper ? slicedObject.CreateUpperHull(toSlice, crossSectionMaterial, instance, false) :
                                                slicedObject.CreateLowerHull(toSlice, crossSectionMaterial, instance, false);
            if (hullGameObject == null)
            {
                // Debug.Log("<color=orange>hullGameObject == null</color>");
                slicesPool.Destroy(instance);
                return;
            }
            instance.AddCrossSectionMaterial(crossSectionMaterial, true);
            hullGameObject.Transform.position = toSlice.transform.position;
            instance.OnDie.AddListener(CollectSlice);
            instance.Explode(direction);
            instance.Destroy(false);
        }

        public void CollectSlice(SpawnableHandler slice)
        {
            slice.OnDie?.RemoveListener(CollectSlice);
            slicesPool.Destroy((SliceHandler)slice);

        }


    }
}
