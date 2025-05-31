using System;
using UnityEngine;
using UnityEngine.Events;

namespace Kitty
{
    [Serializable]
    public class InteractionCommand
    {
        [SerializeField] private string m_command = "Command";

        public string Command => m_command;

        public UnityEvent OnCommandExecuted;

        public void Execute()
            => OnCommandExecuted?.Invoke();

        public bool Evaluate(string _command)
            => string.Equals(m_command, _command, StringComparison.CurrentCultureIgnoreCase);
    }
}
