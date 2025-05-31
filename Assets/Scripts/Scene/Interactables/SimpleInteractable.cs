using UnityEngine;

namespace Kitty
{
    public class SimpleInteractable : Interactable
    {
        [SerializeField] private InteractionCommand[] m_commands = new InteractionCommand[1];

        protected override InteractionCommand[] GetCommands()
            => m_commands;

        private void OnValidate()
        {
            if(m_commands is { Length: > 0 }) { return; }

            m_commands = new InteractionCommand[1];
            Debug.LogWarning("Cannot have empty interaction list");
        }
    }
}
