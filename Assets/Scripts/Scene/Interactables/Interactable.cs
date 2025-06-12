using Coremera.Interaction.Mouse;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(ClickableObject))]
    public abstract class Interactable : MonoBehaviour
    {
        private TypingInteractions m_interactions;
        
        private ClickableObject m_clickable;

        protected virtual void Awake()
        {
            m_interactions = FindAnyObjectByType<TypingInteractions>();
            
            m_clickable = GetComponent<ClickableObject>();
            
            m_clickable.OnClicked.AddListener(OnClicked);
        }

        private void OnClicked()
            => m_interactions.AssignCommands(GetCommands());

        protected abstract InteractionCommand[] GetCommands();
    }
}
