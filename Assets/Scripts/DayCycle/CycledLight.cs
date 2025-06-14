using System;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(Light))]
    public class CycledLight : MonoBehaviour
    {
        [Serializable]
        private class LightValues
        {
            [SerializeField] private Color m_filter = Color.white;
            [SerializeField] private float m_temperature = 5000.0f;
            [SerializeField] private float m_intensity = 0.5f;

            public Color Filter => m_filter;
            public float Temperature => m_temperature;
            public float Intensity => m_intensity;
        }
        
        private DayNightCycle m_dayNightCycle;

        private Light m_light;

        [SerializeField] private LightValues[] m_values;

        private void Awake()
        {
            m_dayNightCycle = GetComponentInParent<DayNightCycle>();
            m_dayNightCycle.OnTimeChanged.AddListener(OnTimeChanged);

            m_light = GetComponent<Light>();
        }

        private void OnTimeChanged(TimeOfDay _time)
        {
            var values = m_values[(int)_time];
            m_light.color = values.Filter;
            m_light.colorTemperature = values.Temperature;
            m_light.intensity = values.Intensity;
        }

        private void OnValidate()
        {
            var dayTimesCount = Enum.GetValues(typeof(TimeOfDay)).Length;
            if(m_values != null && m_values.Length == dayTimesCount) { return; }
            m_values = new LightValues[dayTimesCount];
        }
    }
}
