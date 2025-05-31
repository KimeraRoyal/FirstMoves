using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Kitty
{
    [RequireComponent(typeof(Image))]
    public class SleepFade : MonoBehaviour
    {
        private DayCounter m_counter;

        private Image m_image;

        [SerializeField] private float m_fadeDuration = 0.2f;
        [SerializeField] private Ease m_fadeEase = Ease.Linear;
        [SerializeField] private float m_fadeHoldDuration = 0.1f;
        
        private bool m_sleeping;

        private Sequence m_sequence;

        private void Awake()
        {
            m_counter = FindAnyObjectByType<DayCounter>();

            m_image = GetComponent<Image>();
        }

        [Button("Sleep")]
        public void Sleep()
        {
            if(m_sleeping) { return; }

            if (m_sequence is { active: true })
            {
                m_sequence.Kill();
            }

            m_sequence = DOTween.Sequence();

            m_image.raycastTarget = true;
            m_sequence.Append(m_image.DOFade(1.0f, m_fadeDuration).SetEase(m_fadeEase));
            m_sequence.AppendCallback(NextDay);
            m_sequence.AppendInterval(m_fadeHoldDuration);
            m_sequence.AppendCallback(ReleaseFade);
            m_sequence.Append(m_image.DOFade(0.0f, m_fadeDuration).SetEase(m_fadeEase));
            
        }

        private void NextDay()
        {
            m_counter.Increment();
            m_sleeping = false;
        }

        private void ReleaseFade()
        {
            m_image.raycastTarget = false;
        }
    }
}
