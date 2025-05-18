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
        public UnityEvent OnClearMessages;

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

        public void TypeMessage(string _message)
        {
            if(string.IsNullOrEmpty(_message)) { return; }
            OnMessageSent?.Invoke(_message);
        }

        public void TypeCurrentMessage()
        {
            if(string.IsNullOrEmpty(m_input.text)) { return; }
            TypeMessage($"> {m_input.text}");
            m_input.text = "";
        }

        public void ClearMessages()
        {
            OnClearMessages?.Invoke();
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
            // TODO: Generic class to open window when clicking objects
            // TODO: Move to generic window class
            // TODO: Block player movement while shown
            if(m_shown == _show) { return; }
            m_shown = _show;
            
            m_canvasGroup.alpha = m_shown ? 1.0f : 0.0f;
            m_canvasGroup.interactable = m_shown;
        }
    }
}
