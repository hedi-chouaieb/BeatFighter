using EzySlice;
using UnityEngine;

namespace Hedi.me.BoxWang
{

    public static class SlicedHullExtension
    {
        public static GameObject CreateUpperHull(this SlicedHull slicedHull, GameObject original, Material crossSectionMat, GameObject newObject, bool useOriginalMaterials = true)
        {
            newObject.name = nameof(slicedHull.upperHull);
            return CreateHull(original, crossSectionMat, newObject, slicedHull.upperHull, useOriginalMaterials);
        }
        public static GameObject CreateLowerHull(this SlicedHull slicedHull, GameObject original, Material crossSectionMat, GameObject newObject, bool useOriginalMaterials = true)
        {
            newObject.name = nameof(slicedHull.lowerHull);
            return CreateHull(original, crossSectionMat, newObject, slicedHull.lowerHull, useOriginalMaterials);
        }
        public static IMeshFilterRenderer CreateUpperHull(this SlicedHull slicedHull, IMeshFilterRenderer original, Material crossSectionMat, IMeshFilterRenderer newObject, bool useOriginalMaterials = true)
        {
            newObject.Transform.name = nameof(slicedHull.upperHull);
            return CreateHull(original, crossSectionMat, newObject, slicedHull.upperHull, useOriginalMaterials);
        }
        public static IMeshFilterRenderer CreateLowerHull(this SlicedHull slicedHull, IMeshFilterRenderer original, Material crossSectionMat, IMeshFilterRenderer newObject, bool useOriginalMaterials = true)
        {
            newObject.Transform.name = nameof(slicedHull.lowerHull);
            return CreateHull(original, crossSectionMat, newObject, slicedHull.lowerHull, useOriginalMaterials);
        }

        public static GameObject CreateHull(GameObject original, Material crossSectionMat, GameObject newObject, Mesh hull, bool useOriginalMaterials = true)
        {
            if (newObject == null)
            {
                return null;
            }

            if (hull == null)
            {
                return null;
            }

            newObject.GetComponent<MeshFilter>().mesh = hull;


            newObject.transform.localPosition = original.transform.localPosition;
            newObject.transform.localRotation = original.transform.localRotation;
            newObject.transform.localScale = original.transform.localScale;
            if (useOriginalMaterials)
            {
                Material[] shared = original.GetComponent<MeshRenderer>().sharedMaterials;
                Mesh mesh = original.GetComponent<MeshFilter>().sharedMesh;

                // nothing changed in the hierarchy, the cross section must have been batched
                // with the submeshes, return as is, no need for any changes
                if (mesh.subMeshCount == hull.subMeshCount)
                {
                    // the the material information
                    newObject.GetComponent<Renderer>().sharedMaterials = shared;

                    return newObject;
                }

                // otherwise the cross section was added to the back of the submesh array because
                // it uses a different material. We need to take this into account
                Material[] newShared = new Material[shared.Length + 1];

                // copy our material arrays across using native copy (should be faster than loop)
                System.Array.Copy(shared, newShared, shared.Length);
                newShared[shared.Length] = crossSectionMat;
                // the the material information
                newObject.GetComponent<Renderer>().sharedMaterials = newShared;
            }

            return newObject;
        }

        public static IMeshFilterRenderer CreateHull(IMeshFilterRenderer original, Material crossSectionMat, IMeshFilterRenderer newObject, Mesh hull, bool useOriginalMaterials = true)
        {
            if (newObject == null)
            {
                return null;
            }

            if (hull == null)
            {
                return null;
            }

            newObject.MeshFilter.mesh = hull;


            newObject.Transform.localPosition = original.Transform.localPosition;
            newObject.Transform.localRotation = original.Transform.localRotation;
            newObject.Transform.localScale = original.Transform.localScale;
            if (useOriginalMaterials)
            {
                Material[] shared = original.Renderer.sharedMaterials;
                Mesh mesh = original.MeshFilter.sharedMesh;

                // nothing changed in the hierarchy, the cross section must have been batched
                // with the submeshes, return as is, no need for any changes
                if (mesh.subMeshCount == hull.subMeshCount)
                {
                    // the the material information
                    newObject.Renderer.sharedMaterials = shared;

                    return newObject;
                }

                // otherwise the cross section was added to the back of the submesh array because
                // it uses a different material. We need to take this into account
                Material[] newShared = new Material[shared.Length + 1];

                // copy our material arrays across using native copy (should be faster than loop)
                System.Array.Copy(shared, newShared, shared.Length);
                newShared[shared.Length] = crossSectionMat;

                // the the material information
                newObject.Renderer.sharedMaterials = newShared;
            }

            return newObject;
        }
    }
}
