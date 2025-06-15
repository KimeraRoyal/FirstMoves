using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(Renderer))] [ExecuteInEditMode]
    public class LaptopScreen : MonoBehaviour
    {
        private static readonly int s_mainTex = Shader.PropertyToID("_MainTex");
        private static readonly int s_emissionAmount = Shader.PropertyToID("_Emission_Amount");
        private static readonly int s_overlayAmount = Shader.PropertyToID("_Overlay_Amount");
        
        private Renderer m_renderer;

        [SerializeField] private int m_materialIndex = 1;
        
        [SerializeReference] private Material m_offMaterial;
        [SerializeReference] private Material m_onMaterial;
        private Material[] m_materials;
        
        [SerializeField] private bool m_on;

        [SerializeField] private Texture2D m_screenTexture;
        [SerializeField] [Range(0.0f, 1.0f)] private float m_overlayAmount = 1.0f;
        [SerializeField] [Range(0.0f, 1.0f)] private float m_emissionAmount = 1.0f;

        private MaterialPropertyBlock m_block;

        private void Awake()
        {
            m_renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            ChangeMaterial();
            UpdateMaterialProperties();
        }

        private void ChangeMaterial()
        {
            m_materials = m_renderer.sharedMaterials;
            m_materials[m_materialIndex] = m_on ? m_onMaterial : m_offMaterial;
            m_renderer.materials = m_materials;
        }

        private void UpdateMaterialProperties()
        {
            if(!m_on) { return; }

            if (m_block == null)
            {
                m_block = new MaterialPropertyBlock();
            }
            
            m_renderer.GetPropertyBlock(m_block);
            m_block.SetTexture(s_mainTex, m_screenTexture);
            m_block.SetFloat(s_emissionAmount, m_emissionAmount);
            m_block.SetFloat(s_overlayAmount, m_overlayAmount);
            m_renderer.SetPropertyBlock(m_block);
        }
    }
}
