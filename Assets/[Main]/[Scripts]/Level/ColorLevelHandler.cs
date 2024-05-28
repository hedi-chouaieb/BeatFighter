using UnityEngine;

namespace Hedi.me.BoxWang
{
    public class ColorLevelHandler : MonoBehaviour
    {
        [SerializeField] private ColorEntityData primaryColor;
        [SerializeField] private ColorEntityData secondaryColor;
        [SerializeField] private ColorEntityData[] colors;
        [SerializeField] private int indexPrimaryColor;
        [SerializeField] private int indexSecondaryColor;

        public int IndexPrimaryColor { get => indexPrimaryColor; set => indexPrimaryColor = value; }
        public int IndexSecondaryColor { get => indexSecondaryColor; set => indexSecondaryColor = value; }

        public void InitColors()
        {
            if (IndexPrimaryColor > colors.Length)
            {
                Debug.LogError($"{nameof(IndexPrimaryColor)} ({IndexPrimaryColor}) is Out of Range {nameof(colors.Length)} ({colors.Length}).", this.gameObject);
                return;
            }

            if (IndexSecondaryColor > colors.Length)
            {
                Debug.LogError($"{nameof(IndexSecondaryColor)} ({IndexSecondaryColor}) is Out of Range {nameof(colors.Length)} ({colors.Length}).", this.gameObject);
                return;
            }

            primaryColor.Value = colors[IndexPrimaryColor].Value;
            secondaryColor.Value = colors[IndexSecondaryColor].Value;
        }
    }
}
