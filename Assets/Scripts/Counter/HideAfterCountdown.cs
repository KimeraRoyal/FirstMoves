using UnityEngine;

namespace Kitty
{
    public class HideAfterCountdown : MonoBehaviour
    {
        private DayCounter m_counter;

        [SerializeField] private int m_afterDays;

        private void Awake()
        {
            m_counter = FindAnyObjectByType<DayCounter>();
            m_counter.OnStateUpdated.AddListener(OnStateUpdated);
            m_counter.OnDisplayDateUpdated.AddListener(OnDisplayDateUpdated);
        }

        private void OnStateUpdated(CounterState _state)
        {
            if(m_afterDays > 0 || _state != CounterState.PostCountdown) { return; }
            gameObject.SetActive(false);
        }

        private void OnDisplayDateUpdated(int _displayDate)
        {
            if(m_afterDays < 1 || m_counter.CurrentState != CounterState.PostCountdown || _displayDate <= m_afterDays) { return; }
            gameObject.SetActive(false);
        }
    }
}
