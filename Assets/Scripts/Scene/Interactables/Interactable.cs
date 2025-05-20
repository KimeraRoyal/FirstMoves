using CyberAvebury;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(ClickableObject))]
    public abstract class Interactable : MonoBehaviour
    {
        private TypingWindow m_typingWindow;
        
        private ClickableObject m_clickable;

        protected virtual void Awake()
        {
            m_typingWindow = FindAnyObjectByType<TypingWindow>();
            
            m_clickable = GetComponent<ClickableObject>();
            
            m_clickable.OnClicked.AddListener(OnClicked);
        }

        private void OnClicked()
        {
            m_typingWindow.Show();

            m_typingWindow.ClearMessages();
            var commands = GetCommands();
            foreach (var command in commands)
            {
                m_typingWindow.TypeMessage(command);
            }
        }

        protected abstract string[] GetCommands();
    }
}
