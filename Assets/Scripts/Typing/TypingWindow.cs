using System;
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

        private bool m_shouldShow;
        private bool m_shown;

        public bool Shown
        {
            get => m_shouldShow;
            set => m_shouldShow = value;
        }

        public UnityEvent<string> OnMessageSent;
        public UnityEvent<string> OnCurrentTypedMessageSent;
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
            m_canvasGroup.blocksRaycasts = m_shown;
        }

        private void LateUpdate()
        {
            UpdateShown();
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
            OnCurrentTypedMessageSent?.Invoke(m_input.text);
            
            m_input.text = "";
        }

        public void ClearMessages()
        {
            OnClearMessages?.Invoke();
            m_input.text = "";
        }

        [Button("Show")]
        public void Show() => m_shouldShow = true;

        [Button("Hide")]
        public void Hide() => m_shouldShow = false;

        [Button("Toggle")]
        public void Toggle() => m_shouldShow = !m_shown;

        private void UpdateShown()
        {
            // TODO: Generic class to open window when clicking objects
            // TODO: Move to generic window class
            // TODO: Block player movement while shown
            if(m_shown == m_shouldShow) { return; }
            m_shown = m_shouldShow;
            
            m_canvasGroup.alpha = m_shown ? 1.0f : 0.0f;
            m_canvasGroup.interactable = m_shown;
            m_canvasGroup.blocksRaycasts = m_shown;
        }
    }
}
