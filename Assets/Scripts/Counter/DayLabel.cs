using System;
using TMPro;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(TMP_Text))]
    public class DayLabel : MonoBehaviour
    {
        private DayCounter m_counter;

        private TMP_Text m_text;

        [SerializeField] private string m_format = "{0}";

        private void Awake()
        {
            m_counter = FindAnyObjectByType<DayCounter>();
            
            m_text = GetComponent<TMP_Text>();
            
            m_counter.OnDayOfWeekUpdated.AddListener(OnDayOfWeekUpdated);
        }

        private void Start()
        {
            OnDayOfWeekUpdated(m_counter.DayOfWeek);
        }

        private void OnDayOfWeekUpdated(DayOfWeek _day)
        {
            m_text.text = string.Format(m_format, Enum.GetName(typeof(DayOfWeek), _day));
        }
    }
}
