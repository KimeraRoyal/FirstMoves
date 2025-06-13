using System.Collections.Generic;
using Coremera.Flags;
using UnityEngine;
using UnityEngine.Events;

namespace Kitty
{
    [RequireComponent(typeof(Flags))]
    public class Tasks : MonoBehaviour
    {
        private Flags m_flags;
        
        [SerializeField] private Task[] m_allTasks;

        public IReadOnlyList<Task> AllTasks => m_allTasks;

        public UnityEvent<int> OnTaskMarked;
        public UnityEvent<int> OnTaskUnmarked;

        private void Awake()
        {
            m_flags = GetComponent<Flags>();
            
            for (var i = 0; i < m_allTasks.Length; i++)
            {
                var taskIndex = i;
                m_allTasks[i].OnMarked += () => { MarkFlag(taskIndex); };
                m_allTasks[i].OnUnmarked += () => { UnmarkFlag(taskIndex); };
                m_allTasks[i].IsMarked += () => m_flags.IsFlagSet(taskIndex);
            }
        }

        public void Mark(int _index)
            => m_allTasks[_index].Marked = true;

        public void Unmark(int _index)
            => m_allTasks[_index].Marked = false;

        public void UnmarkAll()
        {
            foreach (var task in m_allTasks)
            {
                task.Marked = false;
            }
        }

        private void MarkFlag(int _index)
        {
            m_flags.SetFlag(_index);
            OnTaskMarked?.Invoke(_index);
        }

        private void UnmarkFlag(int _index)
        {
            m_flags.ClearFlag(_index);
            OnTaskUnmarked?.Invoke(_index);
        }
    }
}
