using UnityEngine;

namespace Kitty
{
    public class PlayerInteractionPoint : MonoBehaviour
    {
        [SerializeField] private float m_maxDistanceFromObject = 10.0f;

        public bool PointInRange(Vector3 _point)
            => Vector3.Distance(transform.position, _point) <= m_maxDistanceFromObject;

        public bool ColliderInRange(Collider _collider)
            => PointInRange(_collider.ClosestPoint(transform.position));
    }
}