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
        [SerializeField] private bool m_marked;
        [SerializeField] private bool m_isVisible;

        [SerializeField] [EnumToggleButtons] private DaysOfWeek m_validDays = DaysOfWeek.All;
        [SerializeField] [EnumToggleButtons] private TimesOfDay m_validTimes = TimesOfDay.None;

        public string Name => m_name;
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

        public bool IsValid(DayOfWeek _day, TimeOfDay _time)
            => IsDayValid(_day) && IsTimeValid(_time);

        public bool IsDayValid(DayOfWeek _day)
            => m_validDays.IsValid(_day);

        public bool IsTimeValid(TimeOfDay _time)
            => m_validTimes.IsValid(_time);
    }
}