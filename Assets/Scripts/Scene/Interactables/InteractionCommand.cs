using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Kitty
{
    [Serializable]
    public class InteractionCommand
    {
        [SerializeField] private string m_command = "Command";

        [SerializeField] private TaskSelection[] m_require;
        [SerializeField] private TaskSelection[] m_blockedBy;
        
        [SerializeField] private TaskSelection[] m_markOnExecute;
        [SerializeField] private TaskSelection[] m_unmarkOnExecute;

        public string Command => m_command;

        public UnityEvent OnCommandExecuted;

        public void Execute()
        {
            foreach (var task in m_markOnExecute)
            {
                task.Mark();
            }
            foreach (var task in m_unmarkOnExecute)
            {
                task.Unmark();
            }
            OnCommandExecuted?.Invoke();
        }

        public bool Evaluate(string _command)
            => string.Equals(m_command, _command, StringComparison.CurrentCultureIgnoreCase);

        public bool IsValid()
            => HasRequirements() && !IsBlocked();

        public bool HasRequirements()
            => m_require.All(_required => _required.Task.Marked);
        
        public bool IsBlocked()
            => m_blockedBy.Any(_blockedBy => _blockedBy.Task.Marked);
    }
}
