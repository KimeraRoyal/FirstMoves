using UnityEngine;

namespace Coremera
{
    [ExecuteInEditMode]
    public class Billboard2D : MonoBehaviour
    {
        [SerializeField] private Transform m_target;

        private void Update()
        {
            if(!m_target) { return; }

            transform.up = new Vector2(m_target.position.x, m_target.position.z) - new Vector2(transform.position.x, transform.position.z);
        }
    }
}
