using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Kitty
{
    [Serializable]
    public class TaskSelection
    {
        private Tasks m_tasks;
        
        [SerializeField] private int m_taskIndex = -1;

        public int TaskIndex => m_taskIndex;

        private Tasks Tasks
        {
            get
            {
                if (!m_tasks)
                {
                    m_tasks = Object.FindAnyObjectByType<Tasks>();
                }
                return m_tasks;
            }
        }
        
        public Task Task => Tasks.AllTasks[m_taskIndex];

        public void Mark()
            => Tasks.Mark(m_taskIndex);

        public void Unmark()
            => Tasks.Unmark(m_taskIndex);

        public static implicit operator Task(TaskSelection _selection) => _selection.Task;
        
#if UNITY_EDITOR
        [SerializeField] [HideInInspector] private string m_taskName = ""; // ONLY USED FOR PROPERTY DRAWER
#endif
    }
}
