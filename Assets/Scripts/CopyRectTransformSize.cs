using UnityEngine;
using UnityEngine.UI;

namespace Kitty
{
    [RequireComponent(typeof(RectTransform))]
    public class CopyRectTransformSize : MonoBehaviour
    {
        private RectTransform m_rect;

        [SerializeField] private RectTransform m_target;

        [SerializeField] private Vector2 m_padding;

        [SerializeField] private bool m_copyWidth = true;
        [SerializeField] private bool m_copyHeight = true;

        private Vector2 m_prevSize;

        private void Awake()
        {
            m_rect = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if(!m_target || (m_target.sizeDelta - m_prevSize).magnitude < 0.001f) { return; }
            m_prevSize = m_target.sizeDelta;

            var size = m_rect.sizeDelta;
            if (m_copyWidth) { size.x = m_target.sizeDelta.x + m_padding.x; }
            if (m_copyHeight) { size.y = m_target.sizeDelta.y + m_padding.y; }
            m_rect.sizeDelta = size;
            
            LayoutRebuilder.MarkLayoutForRebuild(m_rect);
        }
    }
}
