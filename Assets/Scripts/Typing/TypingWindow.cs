using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Kitty
{
    public class TypingWindow : MonoBehaviour
    {
        private TMP_InputField m_input;
        private CanvasGroup m_canvasGroup;

        private bool m_shown;

        public bool Shown
        {
            get => m_shown;
            set => UpdateShown(value);
        }

        public UnityEvent<string> OnMessageSent;

        private void Awake()
        {
            m_input = GetComponentInChildren<TMP_InputField>();
            m_canvasGroup = GetComponentInChildren<CanvasGroup>();
        }

        private void Start()
        {
            m_canvasGroup.alpha = m_shown ? 1.0f : 0.0f;
            m_canvasGroup.interactable = m_shown;
        }

        public void SendCurrentMessage()
        {
            if(string.IsNullOrEmpty(m_input.text)) { return; }
            OnMessageSent?.Invoke(m_input.text);
            m_input.text = "";
        }

        [Button("Show")]
        public void Show() => UpdateShown(true);
        
        [Button("Hide")]
        public void Hide() => UpdateShown(false);
        
        [Button("Toggle")]
        public void Toggle() => UpdateShown(!m_shown);

        private void UpdateShown(bool _show)
        {
            // TODO: Move to generic window class
            // TODO: Block player movement while shown
            if(m_shown == _show) { return; }
            m_shown = _show;
            
            m_canvasGroup.alpha = m_shown ? 1.0f : 0.0f;
            m_canvasGroup.interactable = m_shown;
        }
    }
}
