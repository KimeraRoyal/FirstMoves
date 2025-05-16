using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        private Rigidbody m_rigidbody;

        [SerializeField] private float m_movementForce = 1.0f;

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // TODO: Support controller input I guess?
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
            var movement = new Vector3(horizontal, 0.0f, vertical).normalized * m_movementForce;
            
            movement.y = m_rigidbody.linearVelocity.y;
            m_rigidbody.linearVelocity = movement;
        }
    }
}
