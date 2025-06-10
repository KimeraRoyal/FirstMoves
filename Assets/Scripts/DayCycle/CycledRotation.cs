using System;
using UnityEngine;

namespace Kitty
{
    public class CycledRotation : MonoBehaviour
    {
        private DayCycle m_dayCycle;

        [SerializeField] private Vector3[] m_angles;

        private void Awake()
        {
            m_dayCycle = GetComponentInParent<DayCycle>();
            m_dayCycle.OnTimeChanged.AddListener(OnTimeChanged);
        }

        private void OnTimeChanged(DayTimes _time)
        {
            transform.localEulerAngles = m_angles[(int)_time];
        }

        private void OnValidate()
        {
            var dayTimesCount = Enum.GetValues(typeof(DayTimes)).Length;
            if(m_angles != null && m_angles.Length == dayTimesCount) { return; }
            m_angles = new Vector3[dayTimesCount];
        }
    }
}
