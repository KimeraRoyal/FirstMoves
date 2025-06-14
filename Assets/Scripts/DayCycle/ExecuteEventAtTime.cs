using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Kitty
{
    public class ExecuteEventAtTime : MonoBehaviour
    {
        private DayNightCycle m_cycle;

        [SerializeField] [EnumToggleButtons] private TimesOfDay m_times = TimesOfDay.None;

        public UnityEvent OnTimeReached;

        private void Awake()
        {
            m_cycle = FindAnyObjectByType<DayNightCycle>();
            
            m_cycle.OnTimeChanged.AddListener(OnTimeChanged);
        }

        private void OnTimeChanged(TimeOfDay _time)
        {
            if(!m_times.IsValid(_time)) { return; }
            OnTimeReached?.Invoke();
        }
    }
}
