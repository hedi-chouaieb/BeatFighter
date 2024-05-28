using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private FloatEntityData minRatioToWin;
        [SerializeField] private IntEntityData defaultScoreToAdd;
        [SerializeField] private IntEntityData maxScorePower;
        [SerializeField] private IntEntityData currentScore;
        [SerializeField] private IntEntityData currentCombo;
        [SerializeField] private IntEntityData maxCombo;
        [SerializeField] private IntEntityData totalSliced;
        [SerializeField] private IntEntityData totalMissed;
        [SerializeField] private IntEntityData totalWrongSlice;
        [SerializeField] private UnityEventScorePower onUpdateScorePower;
        [SerializeField] private UnityEvent onWin;
        [SerializeField] private UnityEvent onLose;

        Dictionary<StringEntityData, int> currentScorePowers;

        private void Start()
        {
            currentScore.Value = 0;
            currentCombo.Value = 0;
            maxCombo.Value = 0;
            totalSliced.Value = 0;
            totalMissed.Value = 0;
            totalWrongSlice.Value = 0;
        }

        public void CheckWinner()
        {
            if (totalSliced.Value + totalMissed.Value == 0)
            {
                onLose?.Invoke();
                return;
            }

            if ((float)totalSliced.Value / (float)(totalSliced.Value + totalMissed.Value) < minRatioToWin.Value)
                onLose?.Invoke();
            else
                onWin?.Invoke();
        }

        public void TargetSliced(WeaponHandler weaponHandler, TargetHandler targetHandler, Vector3 contactPoint)
        {

            if (ReferenceEquals(weaponHandler.Orientation, targetHandler.Orientation))
            {
                AddScorePower(targetHandler.Orientation, 1);
            }
            else
            {
                AddScorePower(targetHandler.Orientation, -1);
                totalWrongSlice.Value++;
            }

            totalSliced.Value++;
            CalculateScore(currentScorePowers[targetHandler.Orientation]);
        }

        public void TargetDestroyed(TargetHandler targetHandler)
        {
            AddScorePower(targetHandler.Orientation, -2);
            totalMissed.Value++;
        }

        private void AddScorePower(StringEntityData orientation, int value)
        {
            if (currentScorePowers == null)
            {
                currentScorePowers = new Dictionary<StringEntityData, int>();
            }

            if (!currentScorePowers.ContainsKey(orientation))
            {
                currentScorePowers.Add(orientation, 0);
            }
            currentCombo.Value = value > 0 ? currentCombo.Value + 1 : 0;
            maxCombo.Value = Mathf.Max(currentCombo.Value, maxCombo.Value);

            int newScorePower = currentScorePowers[orientation] + value;
            currentScorePowers[orientation] = Mathf.Clamp(newScorePower, 0, maxScorePower.Value);

            onUpdateScorePower?.Invoke(orientation, currentScorePowers[orientation]);
        }

        private void CalculateScore(int currentScorePower)
        {
            currentScore.Value += (int)(defaultScoreToAdd.Value * Mathf.Pow(2, currentScorePower));
        }

        [System.Serializable] public class UnityEventScorePower : UnityEvent<StringEntityData, int> { }
    }
}