using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    public class ElectricityVisualizer : MonoBehaviour
    {
        [SerializeField] private StringEntityData orientation;
        [SerializeField] private Transform[] electricPoses;
        [SerializeField] private UnityEvent onFullPower;
        [SerializeField] private UnityEvent onNotFullPower;

        private Vector3[] startingElectricPoses;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            startingElectricPoses = new Vector3[electricPoses.Length];
            for (int indexEP = 0; indexEP < electricPoses.Length; indexEP++)
            {
                startingElectricPoses[indexEP] = electricPoses[indexEP].position;
            }

            UpdateVisualizer(orientation, 0);
            onNotFullPower?.Invoke();
        }

        public void UpdateVisualizer(StringEntityData orientation, int power)
        {
            if (!ReferenceEquals(this.orientation, orientation)) return;

            if (startingElectricPoses == null || startingElectricPoses.Length != electricPoses.Length)
            {
                Init();
            }

            for (int indexEP = 0; indexEP < electricPoses.Length; indexEP++)
            {
                electricPoses[indexEP].position = startingElectricPoses[Mathf.Clamp(indexEP, 0, power)];
            }

            (electricPoses.Length - 1 <= power ? onFullPower : onNotFullPower)?.Invoke();
        }
    }
}