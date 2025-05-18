using UnityEngine;

namespace CyberAvebury
{
    public class Mouse3D : Mouse
    {
        protected override Transform Cast(Vector3 _mousePos)
        {
            var ray = Camera.ScreenPointToRay(_mousePos);
            if (!Physics.Raycast(ray, out var rayHit, Camera.farClipPlane, m_targetMask)) { return null; }

            return rayHit.transform;
        }
    }
}
