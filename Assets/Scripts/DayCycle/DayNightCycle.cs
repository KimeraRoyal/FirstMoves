using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Kitty
{
    public class DayNightCycle : MonoBehaviour
    {
        [SerializeField] [EnumToggleButtons] private DayTimes m_currentTime;

        public DayTimes CurrentTime => m_currentTime;

        public UnityEvent<DayTimes> OnTimeChanged;
        public UnityEvent OnDayCycled;

        private void Start()
        {
            OnTimeChanged?.Invoke(m_currentTime);
        }

        [Button("Increment")]
        public void Increment()
        {
            var dayTimesCount = Enum.GetValues(typeof(DayTimes)).Length;
            var currentTime = (int)m_currentTime;

            currentTime++;
            var nextDay = false;
            if (currentTime >= dayTimesCount)
            {
                currentTime = 0;
                nextDay = true;
            }

            SetTime((DayTimes)currentTime, nextDay);
        }

        public void SetMorning(bool _nextDay)
            => SetTime(DayTimes.Morning, _nextDay);

        public void SetTime(DayTimes _time, bool _nextDay)
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
