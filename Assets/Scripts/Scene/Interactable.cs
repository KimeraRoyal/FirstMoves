using CyberAvebury;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(ClickableObject))]
    public class Interactable : MonoBehaviour
    {
        private TypingWindow m_typingWindow;
        
        private ClickableObject m_clickable;

        [SerializeField] private InteractableData m_data;

        private void Awake()
        {
            m_typingWindow = FindAnyObjectByType<TypingWindow>();
            
            m_clickable = GetComponent<ClickableObject>();
            
            m_clickable.OnClicked.AddListener(OnClicked);
        }

        private void OnClicked()
        {
            m_typingWindow.Show();
            
            m_typingWindow.ClearMessages();
            m_typingWindow.TypeMessage(m_data.Prompt);
        }
    }
}
