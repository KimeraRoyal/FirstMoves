using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Kitty
{
    public class DayCounter : MonoBehaviour
    {
        private const int c_daysInWeek = 7;
        
        [EnumToggleButtons]
        [ShowInInspector] [ReadOnly] private CounterState m_state;
        
        [SerializeField] private int m_dayCountdownLength = 5;
        [SerializeField] private int m_minCountdownPeriod = 7, m_maxCountdownPeriod = 9;
        [ShowInInspector] [ReadOnly] private int m_countdownPeriod;
        
        [ShowInInspector] [ReadOnly] private int m_currentDay;
        [ShowInInspector] [ReadOnly] private int m_displayDate;
        
        [EnumToggleButtons]
        [ShowInInspector] [ReadOnly] private DayOfWeek m_dayOfWeek;

        public CounterState CurrentState => m_state;

        public int CurrentDisplayDate => m_displayDate;
        public DayOfWeek DayOfWeek => m_dayOfWeek;

        public UnityEvent<CounterState> OnStateUpdated;
        public UnityEvent<int> OnDisplayDateUpdated;
        public UnityEvent<DayOfWeek> OnDayOfWeekUpdated;

        private void Awake()
        {
            m_countdownPeriod = Random.Range(m_minCountdownPeriod, m_maxCountdownPeriod);

            var targetDay = Random.Range((int) DayOfWeek.Monday, (int) DayOfWeek.Friday + 1);
            targetDay = (targetDay - m_countdownPeriod) % c_daysInWeek;
            while (targetDay < 0)
            {
                targetDay += c_daysInWeek;
            }
            m_dayOfWeek = (DayOfWeek) targetDay;
        }

        private void Start()
        {
            UpdateDay();
        }

        [Button("Next Day")]
        public void Increment()
        {
            m_currentDay++;
            
            var dayOfWeek = (int)m_dayOfWeek + 1;
            if (dayOfWeek >= c_daysInWeek)
            {
                dayOfWeek -= c_daysInWeek;
            }
            m_dayOfWeek = (DayOfWeek) dayOfWeek;
            OnDayOfWeekUpdated?.Invoke(m_dayOfWeek);
            
            UpdateDay();
        }

        private void UpdateDay()
        {
            var minBoundary = m_countdownPeriod - m_dayCountdownLength;
            var state =
                m_currentDay >= m_countdownPeriod ? CounterState.PostCountdown :
                m_currentDay >= minBoundary ? CounterState.Countdown :
                CounterState.PreCountdown;
            ChangeState(state);

            var date = m_state switch
            {
                CounterState.PreCountdown => m_dayCountdownLength,
                CounterState.Countdown => m_dayCountdownLength - (m_currentDay - minBoundary),
                CounterState.PostCountdown => 1 - (m_countdownPeriod - m_currentDay),
                _ => throw new ArgumentOutOfRangeException()
            };
            ChangeDisplayDate(date);
        }

        private void ChangeState(CounterState _state)
        {
            if(m_state == _state) { return; }
            m_state = _state;
            OnStateUpdated?.Invoke(m_state);
        }

        private void ChangeDisplayDate(int _date)
        {
            if(m_displayDate == _date) { return; }
            m_displayDate = _date;
            OnDisplayDateUpdated?.Invoke(m_displayDate);
        }
    }
}
