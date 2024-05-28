using System.Collections.Generic;
using UnityEngine;

namespace Hedi.me.BoxWang
{
    public class PoolManager<T> where T : MonoBehaviour
    {
        private T prefab;
        private Transform parent;
        private List<T> activeObjects;
        private Stack<T> deactivatedObjects;

        public PoolManager(T prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;
            this.activeObjects = new List<T>();
            this.deactivatedObjects = new Stack<T>();
        }

        public T GetInstance()
        {
            var instance = (deactivatedObjects.Count == 0) ? GameObject.Instantiate(prefab, parent) : deactivatedObjects.Pop();
            activeObjects.Add(instance);
            instance.gameObject.SetActive(true);
            return instance;
        }

        public void Destroy(T instance)
        {
            instance.gameObject.SetActive(false);
            activeObjects.Remove(instance);
            deactivatedObjects.Push(instance);
        }
    }
}
