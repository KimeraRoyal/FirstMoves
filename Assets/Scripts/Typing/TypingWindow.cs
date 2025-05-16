using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Kitty
{
    public class TypingWindow : MonoBehaviour
    {
        private TMP_InputField m_input;

        public UnityEvent<string> OnMessageSent;

        private void Awake()
        {
            m_input = GetComponentInChildren<TMP_InputField>();
        }

        public void SendCurrentMessage()
        {
            if(string.IsNullOrEmpty(m_input.text)) { return; }
            OnMessageSent?.Invoke(m_input.text);
            m_input.text = "";
        }
    }
}
