using UnityEngine;
using Random = UnityEngine.Random;

namespace Kitty
{
    [RequireComponent(typeof(Rigidbody))]
    public class Cat : MonoBehaviour
    {
        private Rigidbody m_rigidbody;

        [SerializeField] private float m_minForce = 1.0f, m_maxForce = 1.0f;
        [SerializeField] private AnimationCurve m_forceCurve = AnimationCurve.Linear(0.0f, 0.0f, 1.0f, 1.0f);
        [SerializeField] private float m_minForceChangeInterval = 1.0f, m_maxForceChangeInterval = 1.0f;
        private float m_forceChangeInterval;
        private float m_forceChangeTimer;
        private float m_force;

        [SerializeField] private float m_minAngleChangeInterval = 1.0f, m_maxAngleChangeInterval = 1.0f;
        private float m_angleChangeInterval;
        private float m_angleChangeTimer;
        private Vector3 m_direction;

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            AssignRandomForce();
            AssignRandomAngle();
        }

        private void Update()
        {
            ChangeForce();
            ChangeAngle();
        }

        private void FixedUpdate()
        {
            m_rigidbody.AddForce(m_direction * m_force, ForceMode.Acceleration);
        }

        private void ChangeForce()
        {
            m_forceChangeTimer += Time.deltaTime;
            if(m_forceChangeTimer < m_forceChangeInterval) { return; }
            m_forceChangeTimer -= m_forceChangeInterval;

            AssignRandomForce();
        }

        private void AssignRandomForce()
        {
            var t = m_forceCurve.Evaluate(Random.Range(0.0f, 1.0f));
            m_force = Mathf.Lerp(m_minForce, m_maxForce, t);

            m_forceChangeInterval = Random.Range(m_minForceChangeInterval, m_maxForceChangeInterval);
        }

        private void ChangeAngle()
        {
            m_angleChangeTimer += Time.deltaTime;
            if(m_angleChangeTimer < m_angleChangeInterval) { return; }
            m_angleChangeTimer -= m_angleChangeInterval;

            AssignRandomAngle();
        }

        private void AssignRandomAngle()
        {
            var angle = Random.Range(0.0f, Mathf.PI * 2.0f);
            m_direction = new Vector3(Mathf.Cos(angle), 0.0f, Mathf.Sin(angle));

            m_angleChangeInterval = Random.Range(m_minAngleChangeInterval, m_maxAngleChangeInterval);
        }
    }
}
