using UnityEngine;
using UnityEngine.UI;

namespace Kitty
{
    [RequireComponent(typeof(Image))]
    public class TaskCheck : MonoBehaviour
    {
        private TaskListEntry m_entry;

        private Image m_image;
        
        [SerializeField] private Sprite m_markedSprite;
        [SerializeField] private Sprite m_unmarkedSprite;

        private void Awake()
        {
            m_entry = GetComponentInParent<TaskListEntry>();

            m_image = GetComponent<Image>();
            
            m_entry.OnTrackedTaskChanged.AddListener(OnTrackedTaskChanged);
            m_entry.OnTrackedTaskMarked.AddListener(OnMarked);
            m_entry.OnTrackedTaskUnmarked.AddListener(OnUnmarked);
        }

        private void OnTrackedTaskChanged(Task _task)
        {
            if (_task.Marked)
            {
                OnMarked();
            }
            else
            {
                OnUnmarked();
            }
        }

        private void OnMarked()
        {
            m_image.sprite = m_markedSprite;
        }

        private void OnUnmarked()
        {
            m_image.sprite = m_unmarkedSprite;
        }
    }
}
