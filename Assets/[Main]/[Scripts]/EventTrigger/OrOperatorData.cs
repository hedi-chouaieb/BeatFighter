using UnityEngine;

namespace Hedi.me.BoxWang
{
    [CreateAssetMenu(menuName = "BoxWang/OrOperatorData")]
    public class OrOperatorData : OperatorData
    {
        public override bool Check(bool first, bool second)
        {
            return first || second;
        }
    }
}