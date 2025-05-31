using System;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CounterFade : MonoBehaviour
    {
        private DayCounter m_counter;
        
        private CanvasGroup m_group;

        [SerializeField] private int m_daysBeforeFade = 5;
        [SerializeField] private float m_fadePerDay = 0.05f;

        private void Awake()
        {
            m_counter = GetComponentInParent<DayCounter>();

            m_group = GetComponent<CanvasGroup>();
            
            m_counter.OnStateUpdated.AddListener(OnStateUpdated);
            m_counter.OnDisplayDateUpdated.AddListener(OnDisplayDateUpdated);
        }

        private void Start()
        {
            UpdateFade(m_counter.CurrentState, m_counter.CurrentDisplayDate);
        }

        private void OnStateUpdated(CounterState _state)
            => UpdateFade(_state, m_counter.CurrentDisplayDate);

        private void OnDisplayDateUpdated(int _displayDate)
            => UpdateFade(m_counter.CurrentState, _displayDate);

        private void UpdateFade(CounterState _state, int _displayDate)
        {
            m_group.alpha = _state switch
            {
                CounterState.PreCountdown => 0.0f,
                CounterState.Countdown => 1.0f,
                CounterState.PostCountdown => _displayDate <= m_daysBeforeFade
                    ? 1.0f
                    : Mathf.Clamp(1.0f - m_fadePerDay * (_displayDate - m_daysBeforeFade), 0.0f, 1.0f),
                _ => throw new ArgumentOutOfRangeException(nameof(_state), _state, null)
            };
        }
    }
}
