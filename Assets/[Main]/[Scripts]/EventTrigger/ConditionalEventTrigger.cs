using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    public class ConditionalEventTrigger : MonoBehaviour
    {
        [SerializeField] private OperatorData operatorData;
        [SerializeField] private bool[] conditions;
        [SerializeField] private UnityEvent onConditionValid;
        [SerializeField] private UnityEvent onConditionUnvalid;

        public bool[] Conditions { get => conditions; set => conditions = value; }

        public void ActiveCondition(int index)
        {
            UpdateCondition(index, true);
        }

        public void DeactivateCondition(int index)
        {
            UpdateCondition(index, false);
        }

        public void UpdateCondition(int index, bool isActive)
        {
            if (conditions.Length <= index)
            {
                Debug.LogError($"{nameof(index)} ({index}) is Out of Range {nameof(conditions.Length)} ({conditions.Length}).", this.gameObject);
                return;
            }

            Conditions[index] = isActive;
        }

        public void CheckAndTrigger()
        {
            (Check() ? onConditionValid : onConditionUnvalid)?.Invoke();
        }

        public bool Check()
        {
            var result = conditions[0];
            if (conditions.Length == 1)
            {
                return result;
            }

            for (int i = 1; i < conditions.Length; i++)
            {
                result = operatorData.Check(result, conditions[i]);
            }

            return result;
        }
    }
}
