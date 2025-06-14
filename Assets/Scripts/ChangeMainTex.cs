using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(Renderer))]
    public class ChangeMainTex : MonoBehaviour
    {
        private static readonly int s_mainTex = Shader.PropertyToID("_MainTex");
        private Renderer m_renderer;

        [SerializeField] private Texture2D[] m_textures;

        private MaterialPropertyBlock m_block;

        private void Awake()
        {
            m_renderer = GetComponent<Renderer>();

            m_block = new MaterialPropertyBlock();
        }

        public void ChangeTexture(int _index)
        {
            if(_index < 0 || _index >= m_textures.Length) { return; }

            m_renderer.GetPropertyBlock(m_block);
            m_block.SetTexture(s_mainTex, m_textures[_index]);
            m_renderer.SetPropertyBlock(m_block);
        }
    }
}
