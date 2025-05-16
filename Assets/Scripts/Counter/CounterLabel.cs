using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(TMP_Text))]
    public class CounterLabel : MonoBehaviour
    {
        private enum Type
        {
            Digit,
            Label
        }

        private DayCounter m_counter;

        private TMP_Text m_text;

        [EnumToggleButtons]
        [SerializeField] private Type m_labelType;

        private void Awake()
        {
            m_counter = GetComponentInParent<DayCounter>();
            
            m_text = GetComponent<TMP_Text>();
            
            m_counter.OnDisplayDateUpdated.AddListener(OnDisplayDateUpdated);
        }

        private void OnDisplayDateUpdated(int _displayDate)
        {
            m_text.text = m_labelType switch
            {
                Type.Digit => _displayDate.ToString(),
                Type.Label => _displayDate == 1 ? "Day" : "Days",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
