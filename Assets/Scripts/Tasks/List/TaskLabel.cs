using TMPro;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(TMP_Text))]
    public class TaskLabel : MonoBehaviour
    {
        private TaskListEntry m_entry;

        private TMP_Text m_text;

        private void Awake()
        {
            m_entry = GetComponentInParent<TaskListEntry>();

            m_text = GetComponent<TMP_Text>();
            
            m_entry.OnTrackedTaskChanged.AddListener(OnTrackedTaskChanged);
        }

        private void OnTrackedTaskChanged(Task _task)
        {
            m_text.text = _task.Name;
        }
    }
}
