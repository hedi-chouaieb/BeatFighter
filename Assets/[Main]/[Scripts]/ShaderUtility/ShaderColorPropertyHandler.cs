using UnityEngine;

namespace Hedi.me.BoxWang
{
    public class ShaderColorPropertyHandler : ShaderPropertyHandler<Color>
    {
        protected override void UpdateMaterialShaderPropertyValue(Material material, Color value)
        {
            material.SetColor(propertyIndex, value);
        }
    }
}