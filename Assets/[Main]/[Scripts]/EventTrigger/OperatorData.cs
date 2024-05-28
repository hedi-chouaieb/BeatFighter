using UnityEngine;

namespace Hedi.me.BoxWang
{
    public abstract class OperatorData : ScriptableObject
    {
        public abstract bool Check(bool first, bool second);
    }
}