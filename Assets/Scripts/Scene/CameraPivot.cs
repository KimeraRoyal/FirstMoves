using UnityEngine;

namespace Kitty
{
    public class CameraPivot : MonoBehaviour
    {
        private Player m_player;

        [SerializeField] private Vector2 m_minPlayerPosition = -Vector2.one;
        [SerializeField] private Vector2 m_maxPlayerPosition = Vector2.one;

        [SerializeField] private float m_xRotationAtMin = 25;
        [SerializeField] private float m_xRotationAtMax = 35;

        [SerializeField] private float m_yRotationAtMin = 0;
        [SerializeField] private float m_yRotationAtMax = 0;

        [SerializeField] private float m_smoothTime = 1.0f;

        private Vector3 m_currentAngle;
        private Vector3 m_angleVelocity;
        
        private void Awake()
        {
            m_player = FindAnyObjectByType<Player>();
        }

        private void Start()
        {
            m_currentAngle = CalculateCameraAngle();
            transform.localEulerAngles = m_currentAngle;
        }

        private void Update()
        {
            var targetAngle = CalculateCameraAngle();
            m_currentAngle = Vector3.SmoothDamp(m_currentAngle, targetAngle, ref m_angleVelocity, m_smoothTime);
            transform.localEulerAngles = m_currentAngle;
        }

        private Vector3 CalculateCameraAngle()
        {
            var factor = (new Vector2(m_player.transform.localPosition.x, m_player.transform.localPosition.z) - m_minPlayerPosition) / (m_maxPlayerPosition - m_minPlayerPosition);

            var xRotation = Mathf.Lerp(m_xRotationAtMin, m_xRotationAtMax, factor.y);
            var yRotation = Mathf.Lerp(m_yRotationAtMin, m_yRotationAtMax, factor.x);

            return new Vector3(xRotation, yRotation, 0.0f);
        }
    }
}
