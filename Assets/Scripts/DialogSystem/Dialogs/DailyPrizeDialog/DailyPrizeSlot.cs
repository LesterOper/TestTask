using System;
using Counters;
using DG.Tweening;
using Setups;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.DailyPrizeDialog
{
    public class DailyPrizeSlot : MonoBehaviour
    {
        private string TITLE = "Day ";
        [SerializeField] private Image prizeIcon;
        [SerializeField] private TextMeshProUGUI amountText;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private CanvasGroup slotGroup;
        [SerializeField] private Button claim;
        [SerializeField] private Image claimedView;
        private DailyPrize _dailyPrize;
        private int amount = 0;
        private int _day = 0;

        public void Setup(DailyPrize dailyPrize, int day = 1, bool isClickable = false, bool isClaimed = false)
        {
            _day = day;
            title.text = TITLE + day;
            prizeIcon.sprite = dailyPrize.Icon;
            amountText.text = "X" + dailyPrize.Amount;
            _dailyPrize = dailyPrize;
            slotGroup.DOFade(1, 0.5f).SetEase(Ease.InSine);
            claim.OnClickAsObservable().Subscribe(_ => Claim());
            claim.interactable = isClickable;
            if(isClaimed)
                SetClaimedView();
        }

        private void Claim()
        {
            DailyPrizeUtils dailyPrizeUtils = new DailyPrizeUtils();
            dailyPrizeUtils.SetHasPrize(false);
            dailyPrizeUtils.SetLastDailyPrizeClaimed(DateTime.UtcNow.Ticks);
            dailyPrizeUtils.SetCountClaimedDailyPrize(_day);
            dailyPrizeUtils.SetLastDayClaimed(_day);
            SetClaimedView();
            MessageBroker.Default.Publish(new CounterUpdateEvent()
            {
                Amount = _dailyPrize.Amount,
                MoneyType = MoneyType.TICKET
            });
            MessageBroker.Default.Publish(true);
            MessageBroker.Default.Publish(new SliderUpdateEvent());
        }

        private void SetClaimedView()
        {
            claim.interactable = false;
            claimedView.gameObject.SetActive(true);
        }
    }

    public enum DailyPrizeType
    {
        NONE = 0,
        TICKET = 1,
    }
    
}