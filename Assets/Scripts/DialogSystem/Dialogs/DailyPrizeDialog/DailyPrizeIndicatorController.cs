using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.DailyPrizeDialog
{
    public class DailyPrizeIndicatorController : MonoBehaviour
    {
        [SerializeField] private Image indicator;

        private void Start()
        {
            MessageBroker.Default.Receive<bool>().Subscribe(_ => DeactivateIndicator());
        }

        public void ActivateIndicator()
        {
            indicator.gameObject.SetActive(true);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(indicator.DOFade(1, 0.4f).SetEase(Ease.InSine));
            sequence.Append(indicator.DOFade(0, 0.4f).SetEase(Ease.InSine));
            sequence.SetLoops(-1);
        }
        
        public void DeactivateIndicator()
        {
            indicator.gameObject.SetActive(false);
        }
    }
}