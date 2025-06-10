using System;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(Camera))]
    public class CycledCamera : MonoBehaviour
    {
        private DayNightCycle m_dayNightCycle;

        private Camera m_camera;

        [SerializeField] private Color[] m_backgroundColors;

        private void Awake()
        {
            m_dayNightCycle = FindAnyObjectByType<DayNightCycle>();
            m_dayNightCycle.OnTimeChanged.AddListener(OnTimeChanged);

            m_camera = GetComponent<Camera>();
        }

        private void OnTimeChanged(DayTimes _time)
        {
            m_camera.backgroundColor = m_backgroundColors[(int)_time];
        }

        private void OnValidate()
        {
            var dayTimesCount = Enum.GetValues(typeof(DayTimes)).Length;
            if(m_backgroundColors != null && m_backgroundColors.Length == dayTimesCount) { return; }
            m_backgroundColors = new Color[dayTimesCount];
        }
    }
}
