using System;
using UnityEngine;

namespace Kitty
{
    public class CycledAmbientLight : MonoBehaviour
    {
        private DayNightCycle m_dayNightCycle;

        [ColorUsage(false, true)]
        [SerializeField] private Color[] m_backgroundColors;

        private void Awake()
        {
            m_dayNightCycle = GetComponentInParent<DayNightCycle>();
            m_dayNightCycle.OnTimeChanged.AddListener(OnTimeChanged);
        }

        private void OnTimeChanged(TimeOfDay _time)
        {
            RenderSettings.ambientLight = m_backgroundColors[(int)_time];
        }

        private void OnValidate()
        {
            var dayTimesCount = Enum.GetValues(typeof(TimeOfDay)).Length;
            if(m_backgroundColors != null && m_backgroundColors.Length == dayTimesCount) { return; }
            m_backgroundColors = new Color[dayTimesCount];
        }
    }
}
