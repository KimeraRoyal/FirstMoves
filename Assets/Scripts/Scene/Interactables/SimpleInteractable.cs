using UnityEngine;

namespace Kitty
{
    public class SimpleInteractable : Interactable
    {
        [SerializeField] private InteractionCommand m_command;

        protected override InteractionCommand[] GetCommands()
            => new []{ m_command };
    }
}
