using System;
using UnityEngine;
using UnityEngine.Events;

namespace Hedi.me.BoxWang
{
    public class TimeDemoHolder : MonoBehaviour
    {
        [SerializeField] private int year;
        [SerializeField] private int month;
        [SerializeField] private int day;
        [SerializeField] private UnityEvent onValid;
        [SerializeField] private UnityEvent onError;

        public void Check()
        {
            var dateNow = DateTime.UtcNow; ;
            if (PlayerPrefs.HasKey("lastDate") && dateNow < new DateTime(long.Parse(PlayerPrefs.GetString("lastDate"))))
            {
                onError?.Invoke();
                return;
            }

            PlayerPrefs.SetString("lastDate", dateNow.Ticks.ToString());

            if (DateTime.UtcNow < new DateTime(year, month, day))
            {
                onValid?.Invoke();
                return;
            }
            onError?.Invoke();
        }
    }
}
