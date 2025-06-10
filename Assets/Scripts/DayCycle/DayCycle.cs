using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Kitty
{
    public class DayCycle : MonoBehaviour
    {
        [OnValueChanged("ChangeTime")]
        [SerializeField] [EnumToggleButtons] private DayTimes m_currentTime;

        public DayTimes CurrentTime => m_currentTime;

        public UnityEvent<DayTimes> OnTimeChanged;

        private void Start()
        {
            OnTimeChanged?.Invoke(m_currentTime);
        }

        private void Update()
        {
            if(!Input.GetKeyDown(KeyCode.P)) { return; }
            IncrementTime();
        }

        public void IncrementTime()
        {
            var dayTimesCount = Enum.GetValues(typeof(DayTimes)).Length;
            m_currentTime = (DayTimes)(((int)m_currentTime + 1) % dayTimesCount);
            OnTimeChanged?.Invoke(m_currentTime);
        }

        private void ChangeTime()
        {
            OnTimeChanged?.Invoke(m_currentTime);
        }
    }
}
