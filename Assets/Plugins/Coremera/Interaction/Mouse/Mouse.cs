using UnityEngine;
using UnityEngine.Events;

namespace CyberAvebury
{
    [RequireComponent(typeof(Camera))]
    public abstract class Mouse : MonoBehaviour
    {
        private Camera m_camera;
        
        [SerializeField] protected LayerMask m_targetMask;

        public UnityEvent<ClickableObject> OnObjectClicked;

        private int m_lock;

        protected Camera Camera => m_camera;

        public bool Locked => m_lock > 0;
        
        public UnityEvent<Vector2Int> OnMouseClicked;
        public UnityEvent<Vector2> OnMouseClickedWorld;

        public void Lock()
            => m_lock++;

        public void Unlock()
            => m_lock--;

        protected virtual void Awake()
        {
            m_camera = GetComponent<Camera>();
        }

        protected virtual void Update()
        {
            if (Locked || !Input.GetMouseButtonDown(0)) { return; }

            var mousePos = Input.mousePosition;
            OnMouseClicked?.Invoke(new Vector2Int((int) mousePos.x, (int) mousePos.y));
            OnMouseClickedWorld?.Invoke(Camera.ScreenToWorldPoint(mousePos));
            
            var hitTransform = Cast(mousePos);
            if(!hitTransform) { return; }
            
            var clickableObject = hitTransform.GetComponentInParent<ClickableObject>();
            if(!clickableObject) { return; }
            
            clickableObject.Click();
            OnObjectClicked?.Invoke(clickableObject);
        }

        protected abstract Transform Cast(Vector3 _mousePos);
    }
}
