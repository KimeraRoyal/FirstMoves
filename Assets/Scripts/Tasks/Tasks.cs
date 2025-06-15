using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Kitty
{
    public class Tasks : MonoBehaviour
    {
        [SerializeField] private Task[] m_allTasks;
        private Task[] m_visibleTasks;

        [SerializeField] private float m_allVisibleCompleteProgressionDelay = 1.0f;
        private bool m_visibilityDirty;
        private bool m_allVisibleComplete;
        private bool m_visibleCompletionTimerEnabled;
        private float m_visibleCompletionTimer;

        [SerializeField] private float m_mirageCompletionDelay = 1.0f;
        private Task[] m_mirageTasks;
        private bool m_miragesReady;
        private bool m_miragesComplete;
        private float m_mirageCompletionTimer;

        public IReadOnlyList<Task> AllTasks => m_allTasks;

        public IReadOnlyList<Task> VisibleTasks => m_visibleTasks;

        public UnityEvent<int> OnTaskMarked;
        public UnityEvent<int> OnTaskUnmarked;

        public UnityEvent OnVisibleTasksUpdated;
        public UnityEvent OnAllVisibleTasksCompleted;

        private void Awake()
        {
            for (var i = 0; i < m_allTasks.Length; i++)
            {
                var taskIndex = i;
                m_allTasks[i].OnMarked += () => { OnTaskMarked?.Invoke(taskIndex); };
                m_allTasks[i].OnUnmarked += () => { OnTaskUnmarked?.Invoke(taskIndex); };
                m_allTasks[i].OnVisibilityChanged += _ => { m_visibilityDirty = true; };
            }
        }

        private void Update()
        {
            UpdateVisibility();
            
            CheckMirages();
            MirageTimer();
            
            CheckCompletion();
            CompletionTimer();
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

        private void UpdateVisibility()
        {
            if(!m_visibilityDirty) { return; }
            m_visibilityDirty = false;

            m_visibleTasks = m_allTasks.Where(_task => _task.IsVisible).ToArray();
            m_allVisibleComplete = false;
            m_visibleCompletionTimerEnabled = false;
            m_visibleCompletionTimer = 0.0f;
            OnVisibleTasksUpdated?.Invoke();
            
            m_mirageTasks = m_visibleTasks.Where(_task => _task.Mirage).ToArray();
            m_miragesReady = false;
            m_miragesComplete = false;
        }

        private void CheckMirages()
        {
            if(m_miragesReady || m_miragesComplete) { return; }

            if (m_visibleTasks.Any(_task => !_task.Mirage && _task.Marked)) { return; }
            m_miragesReady = true;
        }

        private void MirageTimer()
        {
            if(!m_miragesReady) { return; }

            m_mirageCompletionTimer += Time.deltaTime;
            if(m_mirageCompletionTimer < m_mirageCompletionDelay) { return; }

            m_miragesReady = false;
            m_miragesComplete = true;

            foreach (var mirageTask in m_mirageTasks)
            {
                mirageTask.Unmark();
            }
        }

        private void CheckCompletion()
        {
            if(m_allVisibleComplete || m_visibleTasks.Any(_task => _task.Marked)) { return; }
            m_allVisibleComplete = true;
            m_visibleCompletionTimerEnabled = true;
        }

        private void CompletionTimer()
        {
            if(!m_visibleCompletionTimerEnabled) { return; }

            m_visibleCompletionTimer += Time.deltaTime;
            if (m_visibleCompletionTimer < m_allVisibleCompleteProgressionDelay) { return; }

            m_visibleCompletionTimerEnabled = false;
            OnAllVisibleTasksCompleted?.Invoke();
        }
    }
}
