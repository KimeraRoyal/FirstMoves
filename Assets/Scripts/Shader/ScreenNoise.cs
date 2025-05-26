using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Kitty
{
    [ExecuteInEditMode]
    public class ScreenNoise : MonoBehaviour
    {
        private static readonly int s_noise = Shader.PropertyToID("_Noise");
        private static readonly int s_noiseRgb = Shader.PropertyToID("_Scrolling_Noise");
        
        [SerializeField] private Material m_material;
        
        [SerializeField] private Texture2D m_noiseTexture;
        [SerializeField] [Min(1)] private int m_noiseScale = 1;
        
        [SerializeField] private Texture2D m_rgbNoiseTexture;
        [SerializeField] private Vector2Int m_rgbNoiseTextureSize = Vector2Int.one * 256;

        private Vector2Int m_previousTextureSize = Vector2Int.one * -1;
        private Vector2Int m_previousRgbTextureSize = Vector2Int.one * -1;

        private bool m_forceResize;
        private bool m_forceResizeRgb;
        
        private bool m_forcedReload;
        private bool m_forcedReloadRgb;

        private void Start()
        {
            ForceResize();
        }

        private void Update()
        {
            if(!m_material || !m_noiseTexture || !m_rgbNoiseTexture) { return; }

            UpdateSize();
            UpdateRGBSize();
            
            ReloadMaterial();
            ReloadMaterialRgb();
        }

        private void UpdateSize()
        {
            var textureSize = new Vector2Int(Screen.width, Screen.height) / m_noiseScale;
            if(!m_forceResize && textureSize == m_previousTextureSize) { return; }
            m_previousTextureSize = textureSize;
            
            m_noiseTexture.Reinitialize(textureSize.x, textureSize.y);

            m_forceResize = false;
            m_forcedReload = true;
        }

        private void UpdateRGBSize()
        {
            if(!m_forceResizeRgb && m_rgbNoiseTextureSize == m_previousRgbTextureSize) { return; }
            m_previousRgbTextureSize = m_rgbNoiseTextureSize;
            
            m_rgbNoiseTexture.Reinitialize(m_rgbNoiseTextureSize.x, m_rgbNoiseTextureSize.y);

            m_forceResizeRgb = false;
            m_forcedReloadRgb = true;
        }

        private void ReloadMaterial()
        {
            if(!m_forcedReload && m_noiseTexture) { return; }

            var colors = new Color[m_noiseTexture.width * m_noiseTexture.height];
            for (var i = 0; i < colors.Length; i++)
            {
                var value = Random.Range(0.0f, 1.0f);
                colors[i] = new Color(value, value, value, 1.0f);
            }
            m_noiseTexture.SetPixels(colors);
            m_noiseTexture.Apply();
            
            m_material.SetTexture(s_noise, m_noiseTexture);
            
            m_forcedReload = false;
        }

        private void ReloadMaterialRgb()
        {
            if(!m_forcedReloadRgb && m_rgbNoiseTexture) { return; }

            var colors = new Color[m_rgbNoiseTexture.width * m_rgbNoiseTexture.height];
            for (var i = 0; i < colors.Length; i++)
            {
                colors[i] = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
            }
            m_rgbNoiseTexture.SetPixels(colors);
            m_rgbNoiseTexture.Apply();
            
            m_material.SetTexture(s_noiseRgb, m_rgbNoiseTexture);
            
            m_forcedReloadRgb = false;
        }

        [Button("Force Resize")]
        public void ForceResize()
        {
            m_forceResize = true;
            m_forceResizeRgb = true;
        }

        [Button("Force Reload")]
        public void ForceReload()
        {
            m_forcedReload = true;
            m_forcedReloadRgb = true;
        }
    }
}
