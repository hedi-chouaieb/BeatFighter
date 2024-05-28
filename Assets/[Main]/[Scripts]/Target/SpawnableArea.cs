using UnityEngine;

namespace Hedi.me.BoxWang
{
    [System.Serializable]
    public struct SpawnableArea
    {
        [SerializeField] private Transform firstCorner;
        [SerializeField] private Transform secondCorner;

        public Transform FirstCorner { get => firstCorner; set => firstCorner = value; }
        public Transform SecondCorner { get => secondCorner; set => secondCorner = value; }
    }
}
