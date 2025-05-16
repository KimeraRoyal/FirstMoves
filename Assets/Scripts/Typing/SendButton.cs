using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Kitty
{
    [RequireComponent(typeof(Button))]
    public class SendButton : MonoBehaviour
    {
        private Button m_button;
        
        public UnityEvent TriggerSend;

        private void Awake()
        {
            m_button = GetComponent<Button>();
            
            m_button.onClick.AddListener(Send);
        }

        private void Update()
        {
            if(!Input.GetKeyDown(KeyCode.Return)) { return; }
            Send();
        }

        private void Send()
        {
            TriggerSend?.Invoke();
        }
    }
}
