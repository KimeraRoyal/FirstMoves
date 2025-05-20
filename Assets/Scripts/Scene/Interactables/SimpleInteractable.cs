using UnityEngine;

namespace Kitty
{
    public class SimpleInteractable : Interactable
    {
        [SerializeField] private string m_command = "Command";

        protected override string[] GetCommands()
            => new []{ m_command };
    }
}
