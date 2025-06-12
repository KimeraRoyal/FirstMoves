using System.Collections.Generic;
using Coremera.Flags;
using UnityEngine;
using UnityEngine.Events;

namespace Kitty
{
    [RequireComponent(typeof(Flags))]
    public class Tasks : MonoBehaviour
    {
        [SerializeField] private Task[] m_allTasks;

        public IReadOnlyList<Task> AllTasks => m_allTasks;

        public UnityEvent<int> OnTaskMarked;
        public UnityEvent<int> OnTaskUnmarked;

        private void Awake()
        {
            for (var i = 0; i < m_allTasks.Length; i++)
            {
                var taskIndex = i;
                m_allTasks[i].OnMarked += () => { OnTaskMarked?.Invoke(taskIndex); };
                m_allTasks[i].OnUnmarked += () => { OnTaskUnmarked?.Invoke(taskIndex); };
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
    }
}
