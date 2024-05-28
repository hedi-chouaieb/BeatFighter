using UnityEngine;


namespace Hedi.me.BoxWang
{
    public interface IMeshFilterRenderer
    {
        public MeshFilter MeshFilter { get; }
        public Renderer Renderer{ get; }
        public Transform Transform{ get; }

    }
}
