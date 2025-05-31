using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Kitty
{
    public class DayCycle : MonoBehaviour
    {
        [OnValueChanged("OnTimeChanged")]
        [SerializeField] [EnumToggleButtons] private DayTimes m_currentTime;

        public UnityEvent<DayTimes> OnTimeChanged;

        private void ChangeTime()
        {
            OnTimeChanged?.Invoke(m_currentTime);
        }
    }
}
