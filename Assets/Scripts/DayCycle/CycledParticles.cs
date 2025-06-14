using System;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(ParticleSystem))]
    public class CycledParticles : MonoBehaviour
    {
        private DayNightCycle m_dayNightCycle;

        private ParticleSystem m_particles;

        [SerializeField] private float[] m_simulationSpeeds;

        private void Awake()
        {
            m_dayNightCycle = FindAnyObjectByType<DayNightCycle>();
            m_dayNightCycle.OnTimeChanged.AddListener(OnTimeChanged);

            m_particles = GetComponent<ParticleSystem>();
        }

        private void OnTimeChanged(TimeOfDay _time)
        {
            var speed = m_simulationSpeeds[(int)_time];
            if (speed < 0.001f && m_particles.isPlaying)
            {
                m_particles.Stop();
                m_particles.Clear();
            }
            else if(!m_particles.isPlaying)
            {
                m_particles.Play();
            }
            
            var mainModule = m_particles.main;
            mainModule.simulationSpeed = speed;
        }

        private void OnValidate()
        {
            var dayTimesCount = Enum.GetValues(typeof(TimeOfDay)).Length;
            if(m_simulationSpeeds != null && m_simulationSpeeds.Length == dayTimesCount) { return; }
            m_simulationSpeeds = new float[dayTimesCount];
        }
    }
}
