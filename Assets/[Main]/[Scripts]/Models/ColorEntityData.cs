using UnityEngine;

namespace Hedi.me.BoxWang
{
    [CreateAssetMenu(menuName = "BoxWang/ColorEntityData")]
    public class ColorEntityData : EntityData<Color>
    {
        [SerializeField] protected bool HDRon;
        [SerializeField][ColorUsage(true, true)] protected Color initialValueHDR;

        public override void ResetValue(bool triggerOnUpdate)
        {
            if (!HDRon)
            {
                base.ResetValue(triggerOnUpdate);
                return;
            }

            if (triggerOnUpdate)
            {
                this.Value = this.initialValueHDR;
                return;
            }
            this.value = initialValueHDR;
        }


    }
}