using UnityEngine;

namespace Kitty
{
    public class TaskSelectionComponent : MonoBehaviour
    {
        [SerializeField] private TaskSelection m_task;

        public Task Task => m_task.Task;

        public bool Marked
        {
            get => m_task.Task.Marked;
            set => m_task.Task.Marked = value;
        }
        
        public void Mark() => m_task.Mark();
        public void Unmark() => m_task.Unmark();
    }
}
