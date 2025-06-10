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

        private void Update()
        {
            if(!Input.GetKeyDown(KeyCode.P)) { return; }
            Increment();
        }

        [Button("Increment")]
        public void Increment()
        {
            var dayTimesCount = Enum.GetValues(typeof(DayTimes)).Length;
            var currentTime = (int)m_currentTime;

            currentTime++;
            if (currentTime >= dayTimesCount)
            {
                currentTime = 0;
                OnDayCycled?.Invoke();
            }

            m_currentTime = (DayTimes)currentTime;
            OnTimeChanged?.Invoke(m_currentTime);
        }

        private void ChangeTime()
        {
            OnTimeChanged?.Invoke(m_currentTime);
        }
    }
}
