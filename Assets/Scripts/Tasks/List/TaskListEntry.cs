using UnityEngine;
using UnityEngine.Events;

namespace Kitty
{
    public class TaskListEntry : MonoBehaviour
    {
        private Task m_trackedTask;

        public Task TrackedTask
        {
            get => m_trackedTask;
            set
            {
                if (m_trackedTask == value) { return; }
                
                if (m_trackedTask != null)
                {
                    m_trackedTask.OnMarked -= OnMarked;
                    m_trackedTask.OnUnmarked -= OnUnmarked;
                }

                m_trackedTask = value;
                m_trackedTask.OnMarked += OnMarked;
                m_trackedTask.OnUnmarked += OnUnmarked;
                
                OnTrackedTaskChanged?.Invoke(m_trackedTask);
            }
        }

        public UnityEvent<Task> OnTrackedTaskChanged;
        
        public UnityEvent OnTrackedTaskMarked;
        public UnityEvent OnTrackedTaskUnmarked;

        private void OnMarked()
            => OnTrackedTaskMarked?.Invoke();

        private void OnUnmarked()
            => OnTrackedTaskUnmarked?.Invoke();
    }
}
