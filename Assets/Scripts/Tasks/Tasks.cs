using System;
using System.Collections.Generic;
using System.Linq;
using Coremera.Flags;
using UnityEngine;
using UnityEngine.Events;

namespace Kitty
{
    public class Tasks : MonoBehaviour
    {
        [SerializeField] private Task[] m_allTasks;

        private bool m_visibilityDirty;

        public IReadOnlyList<Task> AllTasks => m_allTasks;
        public IReadOnlyList<Task> VisibleTasks => m_allTasks.Where(_task => _task.IsVisible).ToList();

        public UnityEvent<int> OnTaskMarked;
        public UnityEvent<int> OnTaskUnmarked;

        public UnityEvent OnVisibleTasksUpdated;

        private void Awake()
        {
            for (var i = 0; i < m_allTasks.Length; i++)
            {
                var taskIndex = i;
                m_allTasks[i].OnMarked += () => { OnTaskMarked?.Invoke(taskIndex); };
                m_allTasks[i].OnUnmarked += () => { OnTaskUnmarked?.Invoke(taskIndex); };
                m_allTasks[i].OnVisibilityChanged += _isVisible => { m_visibilityDirty = true; };
            }
        }

        private void Update()
        {
            if(!m_visibilityDirty) { return; }
            m_visibilityDirty = false;

            OnVisibleTasksUpdated?.Invoke();
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
