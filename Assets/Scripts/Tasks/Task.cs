using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kitty
{
    [Serializable]
    public class Task
    {
        [SerializeField] private string m_name = "Task";
        [SerializeField] private string m_text = "Task";
        [SerializeField] private bool m_marked;
        [SerializeField] private bool m_isVisible;

        [Header("Time & Day")]
        [SerializeField] [EnumToggleButtons] private DaysOfWeek m_validDays = DaysOfWeek.All;
        [SerializeField] [EnumToggleButtons] private TimesOfDay m_validTimes = TimesOfDay.None;
                
        [Header("Counter State")]
        [SerializeField] [EnumToggleButtons] private CounterStates m_validStates = CounterStates.All;
        [SerializeField] private int m_minDayCounterInState = -1;  
        [SerializeField] private int m_maxDayCounterInState = -1;  

        public string Name => m_name;
        public string Text => m_text;
        public bool Marked
        {
            get => m_marked;
            set
            {
                if(m_marked == value) { return; }
                m_marked = value;
                
                if(value) { OnMarked?.Invoke(); }
                else { OnUnmarked?.Invoke(); }
            }
        }

        public bool IsVisible
        {
            get => m_isVisible;
            set
            {
                if(m_isVisible == value) { return; }
                m_isVisible = value;

                OnVisibilityChanged?.Invoke(m_isVisible);
            }
        }

        public DaysOfWeek ValidDays => m_validDays;
        public TimesOfDay ValidTimes => m_validTimes;

        public Action OnMarked;
        public Action OnUnmarked;

        public Action<bool> OnVisibilityChanged;

        public void Mark()
            => Marked = true;

        public void Unmark()
            => Marked = false;

        public bool IsValid(DayOfWeek _day, TimeOfDay _time, CounterState _state, int _counterDate)
            => IsDayValid(_day) && IsTimeValid(_time) && IsStateValid(_state, _counterDate);

        public bool IsDayValid(DayOfWeek _day)
            => m_validDays.IsValid(_day);

        public bool IsTimeValid(TimeOfDay _time)
            => m_validTimes.IsValid(_time);

        public bool IsStateValid(CounterState _state, int _counterDate)
            => m_validStates.IsValid(_state) && 
               (_state == CounterState.PreCountdown || 
                (m_minDayCounterInState < 0 || _counterDate >= m_minDayCounterInState) && 
                (m_maxDayCounterInState < 0 || _counterDate <= m_maxDayCounterInState));
    }
}