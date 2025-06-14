using Sirenix.OdinInspector;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(Animator))]
    public class ToggleableInteractable : Interactable
    {
        private static int s_instantVariableHash = Animator.StringToHash("Instant");
        
        private Animator m_animator;

        [SerializeField] private string m_toggleVariableName = "Open";
        private int m_toggleHash;

        [SerializeField] private InteractionCommand m_openCommand;
        [SerializeField] private InteractionCommand m_closeCommand;
        [SerializeField] private InteractionCommand[] m_openedCommands;
        
        private bool m_isOpen;

        public bool IsOpen => m_isOpen;

        protected override void Awake()
        {
            base.Awake();
            
            m_toggleHash = Animator.StringToHash(m_toggleVariableName);
            
            m_animator = GetComponent<Animator>();
            
            m_openCommand.OnCommandExecuted.AddListener(Open);
            m_closeCommand.OnCommandExecuted.AddListener(Close);
        }

        [Button("Open")]
        public void Open()
            => SetOpen(true, false);
        
        public void OpenInstant()
            => SetOpen(true, true);

        [Button("Close")]
        public void Close()
            => SetOpen(false, false);
        
        public void CloseInstant()
            => SetOpen(false, true);

        private void SetOpen(bool _open, bool _instant)
        {
            if(m_isOpen == _open) { return; }
            m_isOpen = _open;

            m_animator.SetBool(m_toggleHash, m_isOpen);
            if(_instant) { m_animator.SetTrigger(s_instantVariableHash); }
        }

        protected override InteractionCommand[] GetCommands()
        {
            if (!m_isOpen) { return new[] { m_openCommand }; }
            
            var commands = new InteractionCommand[1 + m_openedCommands.Length];
            commands[0] = m_closeCommand;
            for (var i = 0; i < m_openedCommands.Length; i++)
            {
                commands[i + 1] = m_openedCommands[i];
            }
            return commands;
        }
    }
}
