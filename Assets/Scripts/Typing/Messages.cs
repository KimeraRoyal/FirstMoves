using TMPro;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(TMP_Text))]
    public class Messages : MonoBehaviour
    {
        private TypingWindow m_window;
        
        private TMP_Text m_text;

        private void Awake()
        {
            m_window = GetComponentInParent<TypingWindow>();
            
            m_text = GetComponent<TMP_Text>();
            
            m_window.OnMessageSent.AddListener(AddMessage);
        }

        private void AddMessage(string _message)
        {
            m_text.text += $"{(string.IsNullOrEmpty(m_text.text) ? "" : "\n")}> {_message}";
            // TODO: Typewriter effect from console
        }
    }
}
