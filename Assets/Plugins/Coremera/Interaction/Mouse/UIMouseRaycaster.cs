using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Coremera.Interaction.Mouse
{
    [RequireComponent(typeof(GraphicRaycaster))]
    public class UIMouseRaycaster : MonoBehaviour
    {
        private GraphicRaycaster m_raycaster;

        private List<RaycastResult> m_raycastResults;
        private bool m_hasCastThisFrame;

        private void Awake()
        {
            m_raycaster = GetComponent<GraphicRaycaster>();
        }

        private void LateUpdate()
        {
            m_hasCastThisFrame = false;
        }

        public List<RaycastResult> Raycast()
        {
            if (m_hasCastThisFrame) { return m_raycastResults; }
            
            var eventData = new PointerEventData(null)
            {
                position = Input.mousePosition
            };
            
            m_raycastResults = new List<RaycastResult>();
            m_raycaster.Raycast(eventData, m_raycastResults);

            m_hasCastThisFrame = true;
            return m_raycastResults;
        }

        public RaycastResult GetFirstRaycastResult()
        {
            var raycastResults = Raycast();
            if (raycastResults.Count < 1) { return new RaycastResult(); }
            return raycastResults[0];
        }

        public GameObject GetFirstRaycastObject()
            => GetFirstRaycastResult().gameObject;

        public List<GameObject> GetRaycastObjects()
        {
            var raycastResults = Raycast();
            return raycastResults.Select(_result => _result.gameObject).ToList();
        }
        
        public T GetFirstRaycastComponent<T>() where T : MonoBehaviour
        {
            var raycastResults = Raycast();
            return raycastResults.Select(_result => _result.gameObject.GetComponent<T>()).FirstOrDefault(_component => _component);
        }

        public List<T> GetRaycastComponents<T>() where T : MonoBehaviour
        {
            var raycastResults = Raycast();
            return raycastResults.Select(_result => _result.gameObject.GetComponent<T>()).Where(_component => _component).ToList();
        }
    }
}
