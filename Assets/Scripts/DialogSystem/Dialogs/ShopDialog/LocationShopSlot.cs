using System;
using Counters;
using DefaultNamespace.Locations;
using DefaultNamespace.Utils;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.ShopDialog
{
    public class LocationShopSlot : NonPremShopSlot
    {
        [SerializeField] private LocationType _locationType;
        [SerializeField] private GameObject locationIcon;
        private NonPremItem item;

        private void OnEnable()
        {
            EventsInvoker.StartListening("UpdateAvailaibilityToBuy", UpdateAvalabilityToBuy);
        }

        private void OnDisable()
        {
            EventsInvoker.StopListening("UpdateAvailaibilityToBuy", UpdateAvalabilityToBuy);
        }

        protected override void Start()
        {
            base.Start();
            item = _locationType.GetAttribute<NonPremItem>();
            cost.text = item.Cost.ToString();
            lvlNeed.text = "LVL. " + item.NumLvlToUnlock.ToString();
            if (_shopUtils.IsLocationClaimed(_locationType))
            {
                ClaimedSlot(true);
            }
            else
            {
                bool locked = _locationUtils.GetLvlPassed() < item.NumLvlToUnlock;
                locationIcon.SetActive(!locked);
                LockUnlockSlot(locked);
            }

            UpdateAvalabilityToBuy();
        }

        private void UpdateAvalabilityToBuy()
        {
            buyButton.interactable = _counterUtils.GetMoney(MoneyType.TICKET) >= item.Cost 
                                     && _locationUtils.GetLvlPassed() >= item.NumLvlToUnlock
                                     && !_shopUtils.IsLocationClaimed(_locationType);
            
        }
        
        protected override void Buy()
        {
            base.Buy();
            _shopUtils.SetLocationClaimed(true, _locationType);

            MessageBroker.Default.Publish(new CounterUpdateEvent()
            {
                Amount = -_locationType.GetAttribute<NonPremItem>().Cost,
                MoneyType = MoneyType.TICKET
            });
            EventsInvoker.TriggerEvent("UpdateAvailaibilityToBuy");
        }
    }
}