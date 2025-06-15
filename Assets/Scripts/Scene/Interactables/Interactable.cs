using Coremera.Interaction.Mouse;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(ClickableObject))]
    public abstract class Interactable : MonoBehaviour
    {
        private TypingInteractions m_interactions;
        
        private PlayerInteractionPoint m_player;
        [SerializeField] private Collider m_collider;
        
        private ClickableObject m_clickable;

        protected virtual void Awake()
        {
            m_interactions = FindAnyObjectByType<TypingInteractions>();
            
            m_player = FindAnyObjectByType<PlayerInteractionPoint>();
            if (!m_collider)
            {
                m_collider = GetComponent<Collider>();
            }
            
            m_clickable = GetComponent<ClickableObject>();
            
            m_clickable.OnClicked.AddListener(OnClicked);
        }

        private void OnClicked()
        {
            if(m_collider && !m_player.ColliderInRange(m_collider)) { return; }
            m_interactions.AssignCommands(GetCommands());
        }

        protected abstract InteractionCommand[] GetCommands();
    }
}
