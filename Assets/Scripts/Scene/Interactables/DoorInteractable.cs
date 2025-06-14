using UnityEngine;
using UnityEngine.Events;

namespace Kitty
{
    [RequireComponent(typeof(Animator))]
    public class DoorInteractable : Interactable
    {
        private static readonly int s_open = Animator.StringToHash("Open");
        
        private Animator m_animator;

        [SerializeField] private InteractionCommand m_openCommand;
        
        public UnityEvent OnFullyOpened;

        protected override void Awake()
        {
            base.Awake();
            m_animator = GetComponent<Animator>();
            
            m_openCommand.OnCommandExecuted.AddListener(() => m_animator.SetTrigger(s_open));
        }

        public void FullyOpen()
        {
            OnFullyOpened?.Invoke();
        }
        
        protected override InteractionCommand[] GetCommands()
            => new[] { m_openCommand };
    }
}
