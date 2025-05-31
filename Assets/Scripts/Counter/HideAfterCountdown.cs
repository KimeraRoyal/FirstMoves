using UnityEngine;

namespace Kitty
{
    public class HideAfterCountdown : MonoBehaviour
    {
        private DayCounter m_counter;

        private void Awake()
        {
            m_counter = FindAnyObjectByType<DayCounter>();
            m_counter.OnStateUpdated.AddListener(OnStateUpdated);
        }

        private void OnStateUpdated(CounterState _state)
        {
            if(_state != CounterState.PostCountdown) { return; }
            gameObject.SetActive(false);
        }
    }
}
