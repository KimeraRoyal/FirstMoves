using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(Animator))]
    public class FridgeInteractable : Interactable
    {
        private static readonly int s_openHash = Animator.StringToHash("Open");
        
        private Animator m_animator;

        [SerializeField] private string m_openCommand = "Open Command";
        [SerializeField] private string m_closeCommand = "Close Command";
        
        private bool m_isOpen;

        public bool IsOpen => m_isOpen;

        protected override void Awake()
        {
            base.Awake();
            
            m_animator = GetComponent<Animator>();
        }

        [Button("Open")]
        public void Open()
            => SetOpen(true);

        [Button("Close")]
        public void Close()
            => SetOpen(false);

        private void SetOpen(bool _open)
        {
            if(m_isOpen == _open) { return; }
            m_isOpen = _open;

            m_animator.SetBool(s_openHash, m_isOpen);
        }

        protected override string[] GetCommands()
            => new[] { m_isOpen ? m_closeCommand : m_openCommand };
    }
}
