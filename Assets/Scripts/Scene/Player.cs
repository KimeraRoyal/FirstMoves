using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        private Rigidbody m_rigidbody;

        [SerializeField] private float m_movementForce = 1.0f;

        [SerializeField] private bool m_movementLocked;

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (m_movementLocked)
            {
                m_rigidbody.linearVelocity = Vector3.zero;
                return;
            }
            
            // TODO: Support controller input I guess?
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
            var movement = new Vector3(horizontal, 0.0f, vertical).normalized * m_movementForce;
            
            movement.y = m_rigidbody.linearVelocity.y;
            m_rigidbody.linearVelocity = movement;
        }

        public void LockMovement()
            => m_movementLocked = true;

        public void UnlockMovement()
            => m_movementLocked = false;
    }
}
