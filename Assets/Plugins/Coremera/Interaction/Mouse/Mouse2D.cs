using UnityEngine;

namespace CyberAvebury
{
    public class Mouse2D : Mouse
    {
        protected override Transform Cast(Vector3 _mousePos)
        {
            var ray = Camera.ScreenPointToRay(_mousePos);
            var rayHit = Physics2D.Raycast(ray.origin, Vector2.up, 0.001f, m_targetMask);
            
            return rayHit.transform;
        }
    }
}