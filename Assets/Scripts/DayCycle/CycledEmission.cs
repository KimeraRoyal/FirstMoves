using System;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(Renderer))]
    public class CycledEmission : MonoBehaviour
    {
        private static readonly int s_emissionHash = Shader.PropertyToID("_EmissionColor");

        private DayNightCycle m_dayNightCycle;
        
        private Renderer m_renderer;
        
        [ColorUsage(false, true)]
        [SerializeField] private Color[] m_colors;

        private MaterialPropertyBlock m_propertyBlock;

        private void Awake()
        {
            m_dayNightCycle = FindAnyObjectByType<DayNightCycle>();
            m_dayNightCycle.OnTimeChanged.AddListener(OnTimeChanged);

            m_renderer = GetComponent<Renderer>();
            
            m_propertyBlock = new MaterialPropertyBlock();
        }

        private void OnTimeChanged(DayTimes _time)
        {
            SetEmissionColor(m_colors[(int)_time]);
        }

        private void SetEmissionColor(Color _color)
        {
            m_renderer.GetPropertyBlock(m_propertyBlock);
            m_propertyBlock.SetColor(s_emissionHash, _color);
            m_renderer.SetPropertyBlock(m_propertyBlock);
        }

        private void OnValidate()
        {
            var dayTimesCount = Enum.GetValues(typeof(DayTimes)).Length;
            if(m_colors != null && m_colors.Length == dayTimesCount) { return; }
            m_colors = new Color[dayTimesCount];
        }
    }
}
