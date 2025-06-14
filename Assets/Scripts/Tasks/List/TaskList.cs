using System.Collections.Generic;
using UnityEngine;

namespace Kitty
{
    public class TaskList : MonoBehaviour
    {
        private Tasks m_tasks;

        [SerializeField] private TaskListEntry m_listEntryPrefab;

        private List<TaskListEntry> m_list;
        
        private void Awake()
        {
            m_tasks = FindAnyObjectByType<Tasks>();
            m_tasks.OnVisibleTasksUpdated.AddListener(UpdateVisibleTasks);

            m_list = new List<TaskListEntry>();
        }

        private void UpdateVisibleTasks()
        {
            var visibleTasks = m_tasks.VisibleTasks;

            for (var i = 0; i < visibleTasks.Count; i++)
            {
                TaskListEntry entry;
                if (i < m_list.Count)
                {
                    entry = m_list[i];
                    entry.gameObject.SetActive(true);
                }
                else
                {
                    entry = Instantiate(m_listEntryPrefab, transform);
                    m_list.Add(entry);
                }
                entry.TrackedTask = visibleTasks[i];
            }

            for (var i = visibleTasks.Count; i < m_list.Count; i++)
            {
                m_list[i].gameObject.SetActive(false);
            }
        }
    }
}
