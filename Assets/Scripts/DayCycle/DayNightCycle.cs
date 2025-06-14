using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Kitty
{
    public class DayNightCycle : MonoBehaviour
    {
        [SerializeField] [EnumToggleButtons] private TimeOfDay m_currentTime;

        public TimeOfDay CurrentTime => m_currentTime;

        public UnityEvent<TimeOfDay> OnTimeChanged;
        public UnityEvent OnDayCycled;

        private void Start()
        {
            OnTimeChanged?.Invoke(m_currentTime);
        }

        [Button("Increment")]
        public void Increment()
        {
            var dayTimesCount = Enum.GetValues(typeof(TimeOfDay)).Length;
            var currentTime = (int)m_currentTime;

            currentTime++;
            var nextDay = false;
            if (currentTime >= dayTimesCount)
            {
                currentTime = 0;
                nextDay = true;
            }

            SetTime((TimeOfDay)currentTime, nextDay);
        }

        public void SetMorning(bool _nextDay)
            => SetTime(TimeOfDay.Morning, _nextDay);

        public void SetDay(bool _nextDay)
            => SetTime(TimeOfDay.Day, _nextDay);

        public void SetEvening(bool _nextDay)
            => SetTime(TimeOfDay.Evening, _nextDay);

        public void SetNight(bool _nextDay)
            => SetTime(TimeOfDay.Night, _nextDay);

        public void SetTime(TimeOfDay _time, bool _nextDay)
        {
            if (_nextDay)
            {
                OnDayCycled?.Invoke();
            }
            else if(m_currentTime == _time) { return; }
            
            m_currentTime = _time;
            OnTimeChanged?.Invoke(m_currentTime);
        }

        private void ChangeTime()
        {
            OnTimeChanged?.Invoke(m_currentTime);
        }
    }
}
