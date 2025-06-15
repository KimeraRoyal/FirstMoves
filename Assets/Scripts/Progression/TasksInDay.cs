using System;
using System.Linq;
using UnityEngine;

namespace Kitty
{
    public class TasksInDay : MonoBehaviour
    {
        private Tasks m_tasks;
        
        private DayCounter m_day;
        private DayNightCycle m_time;

        private void Awake()
        {
            m_tasks = FindAnyObjectByType<Tasks>();
            
            m_day = FindAnyObjectByType<DayCounter>();
            m_time = FindAnyObjectByType<DayNightCycle>();
            
            m_day.OnDayOfWeekUpdated.AddListener(OnDayOfWeekUpdated);
            m_time.OnTimeChanged.AddListener(OnTimeChanged);
        }

        private void OnDayOfWeekUpdated(DayOfWeek _day)
            => UpdateTasks();

        private void OnTimeChanged(TimeOfDay _time)
            => UpdateTasks();

        private void UpdateTasks()
        {
            foreach (var task in m_tasks.AllTasks)
            {
                var valid = task.IsValid(m_day.DayOfWeek, m_time.CurrentTime, m_day.CurrentState, m_day.CurrentDisplayDate);
                if(valid) { task.Marked = true; }
                task.IsVisible = valid;
            }
        }
    }
}
