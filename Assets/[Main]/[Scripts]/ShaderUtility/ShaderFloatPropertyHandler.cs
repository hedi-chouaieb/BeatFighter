using UnityEngine;

namespace Hedi.me.BoxWang
{
    public class ShaderFloatPropertyHandler : ShaderPropertyHandler<float>
    {
        protected override void UpdateMaterialShaderPropertyValue(Material material, float value)
        {
            material.SetFloat(propertyIndex, value);
        }
    }
}
