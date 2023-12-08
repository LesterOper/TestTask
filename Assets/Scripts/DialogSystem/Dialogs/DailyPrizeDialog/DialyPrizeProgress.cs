using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.DailyPrizeDialog
{
    public class DialyPrizeProgress : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI sliderText;
        private DailyPrizeUtils _dailyPrizeUtils;

        public void Setup(Action weekEndEvent)
        {
            _dailyPrizeUtils = new DailyPrizeUtils();
            var days = _dailyPrizeUtils.GetCurrentDailyPrize();
            if (_dailyPrizeUtils.GetCurrentDailyPrize() >= 7)
            {
                weekEndEvent.Invoke();
                _dailyPrizeUtils.SetCountClaimedDailyPrize(0);
                UpdateSliderInfo();
                MessageBroker.Default.Publish(false);
            }
            UpdateSliderInfo();
            MessageBroker.Default.Receive<SliderUpdateEvent>().Subscribe(_=>UpdateSliderInfo());
        }

        private void UpdateSliderInfo()
        {
            _slider.value = _dailyPrizeUtils.GetCountClaimedDailyPrize();
            sliderText.text = _dailyPrizeUtils.GetCountClaimedDailyPrize() + "/7";
        }
    }
    
    public class SliderUpdateEvent{}
}