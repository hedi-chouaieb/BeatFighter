using UnityEngine;

namespace Hedi.me.BoxWang
{
    [CreateAssetMenu(menuName = "BoxWang/AndOperatorData")]
    public class AndOperatorData : OperatorData
    {
        public override bool Check(bool first, bool second)
        {
            return first && second;
        }
    }
}