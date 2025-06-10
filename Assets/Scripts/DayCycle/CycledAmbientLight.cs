using System;
using UnityEngine;

namespace Kitty
{
    public class CycledAmbientLight : MonoBehaviour
    {
        private DayCycle m_dayCycle;

        [ColorUsage(false, true)]
        [SerializeField] private Color[] m_backgroundColors;

        private void Awake()
        {
            m_dayCycle = GetComponentInParent<DayCycle>();
            m_dayCycle.OnTimeChanged.AddListener(OnTimeChanged);
        }

        private void OnTimeChanged(DayTimes _time)
        {
            RenderSettings.ambientLight = m_backgroundColors[(int)_time];
        }

        private void OnValidate()
        {
            var dayTimesCount = Enum.GetValues(typeof(DayTimes)).Length;
            if(m_backgroundColors != null && m_backgroundColors.Length == dayTimesCount) { return; }
            m_backgroundColors = new Color[dayTimesCount];
        }
    }
}
