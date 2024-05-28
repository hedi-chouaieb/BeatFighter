using UnityEngine;

namespace Hedi.me.BoxWang
{
    public class VFXColorPropertyHandler : VFXPropertyHandler<Color>
    {
        protected override bool HasProperty()
        {
            return visualEffect.HasVector4(propertyName);
        }

        protected override void UpdatePropertyValue(Color value)
        {
            visualEffect.SetVector4(propertyName, value);
        }
    }
}
