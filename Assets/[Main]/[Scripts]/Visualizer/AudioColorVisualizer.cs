using UnityEngine;

namespace Hedi.me.BoxWang
{
    public class AudioColorVisualizer : MonoBehaviour
    {
        [SerializeField] private ColorEntityData[] colors;
        [SerializeField] private ColorEntityData currentColor;

        int currentIndex = 0;
        public void ChangeColor()
        {
            currentIndex++;
            if (currentIndex / 2 >= colors.Length)
                currentIndex = 0;
            currentColor.Value = colors[currentIndex / 2].Value;
        }
    }
}
