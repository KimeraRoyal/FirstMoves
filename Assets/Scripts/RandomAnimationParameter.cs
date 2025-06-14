using UnityEngine;
using Random = UnityEngine.Random;

namespace Kitty
{
    [RequireComponent(typeof(Animator))]
    public class RandomAnimationParameter : MonoBehaviour
    {
        private Animator m_animator;

        [SerializeField] private string m_parameterName = "Random";
        private int m_parameterHash;
        
        [SerializeField] private int m_range = 2;
        private int m_current;

        [SerializeField] private float m_minChangeInterval = 1.0f;
        [SerializeField] private float m_maxChangeInterval = 1.0f;
        private float m_changeInterval;
        private float m_changeTimer;
        
        private void Awake()
        {
            m_animator = GetComponent<Animator>();

            m_parameterHash = Animator.StringToHash(m_parameterName);
        }

        private void Start()
        {
            m_current = Random.Range(0, m_range);
            RandomizeChannel();
        }

        private void Update()
        {
            m_changeTimer += Time.deltaTime;
            if(m_changeTimer < m_changeInterval) { return; }
            m_changeTimer -= m_changeInterval;

            RandomizeChannel();
        }

        private void RandomizeChannel()
        {
            m_current = (m_current + Random.Range(1, m_range)) % m_range;
            m_animator.SetFloat(m_parameterHash, m_current);

            m_changeInterval = Random.Range(m_minChangeInterval, m_maxChangeInterval);
        }
    }
}
