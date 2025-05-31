using System;
using UnityEngine;

namespace Kitty
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CounterFade : MonoBehaviour
    {
        private CanvasGroup m_group;

        private void Awake()
        {
            m_group = GetComponent<CanvasGroup>();
        }
    }
}
