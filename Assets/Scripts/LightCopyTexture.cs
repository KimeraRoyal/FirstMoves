using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Serialization;

namespace Kitty
{
    [RequireComponent(typeof(Light))]
    public class LightCopyTexture : MonoBehaviour
    {
        private Light m_light;
        
        [FormerlySerializedAs("m_texture")] [SerializeField] private RenderTexture m_target;

        [ShowInInspector] private Color m_lightColor;
        [SerializeField] private Color m_baseColor = Color.white;
        [SerializeField] [Range(0.0f, 1.0f)] private float m_blend = 0.5f;

        [SerializeField] private Vector2Int m_sampleCount = new Vector2Int(3, 3);
        [SerializeField] private float m_updateInterval = 1.0f;
        private float m_updateTimer;

        [SerializeField] private float m_tweenDuration = 0.1f;

        private Texture2D m_texture;
        private Rect m_readRect;
        private readonly float[] m_samples = new float[3];

        private Tween m_lightTween;

        private Texture2D Texture
        {
            get
            {
                if (m_texture) { return m_texture; }

                m_texture = new Texture2D(m_target.width, m_target.height);
                m_readRect = new Rect(0, 0, m_target.width, m_target.height);
                return m_texture;
            }
        }
        
        private void Awake()
        {
            m_light = GetComponent<Light>();
        }

        private void OnEnable()
        {
            UpdateLight();
        }

        private void Update()
        {
            LightTimer();
            m_light.color = m_lightColor;
        }

        private void LightTimer()
        {
            m_updateTimer += Time.deltaTime;
            if (m_updateTimer < m_updateInterval) { return; }
            m_updateTimer -= m_updateInterval;
            
            UpdateLight();
        }

        private void UpdateLight()
        {
            Profiler.BeginSample("Update Light");
            var activeRT = RenderTexture.active;
            RenderTexture.active = m_target;
            Texture.ReadPixels(m_readRect, 0, 0);
            RenderTexture.active = activeRT;

            var xDistance = Texture.width / m_sampleCount.x;
            var yDistance = Texture.height / m_sampleCount.y;
            for (var y = 0; y < m_sampleCount.y; y++)
            {
                for (var x = 0; x < m_sampleCount.x; x++)
                {
                    var color = Texture.GetPixel(xDistance * x, yDistance * y);
                    m_samples[0] += color.r;
                    m_samples[1] += color.g;
                    m_samples[2] += color.b;
                }
            }
            
            var sampledColor = Color.black;
            var totalSamples = m_sampleCount.x * m_sampleCount.y;
            for (var i = 0; i < 3; i++)
            {
                sampledColor[i] = m_samples[i] / totalSamples;
                m_samples[i] = 0.0f;
            }
            Profiler.EndSample();

            ChangeLightColor(Color.Lerp(sampledColor, m_baseColor, m_blend));
        }

        private void ChangeLightColor(Color _color)
        {
            if(m_lightTween is { active: true }) { return; }
            m_lightTween = DOTween.To(() => m_lightColor, _value => m_lightColor = _value, _color, m_tweenDuration);
        }
    }
}
