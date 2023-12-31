﻿using Counters;
using DefaultNamespace.Locations;
using DefaultNamespace.Utils;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.ShopDialog
{
    public class SkinsShopSlot : NonPremShopSlot
    {
        [SerializeField] private SkinType skinType;
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
            item = skinType.GetAttribute<NonPremItem>();
            cost.text = item.Cost.ToString();
            lvlNeed.text = "LVL. " + item.NumLvlToUnlock.ToString();
            if (_shopUtils.IsSkinClaimed(skinType))
            {
                ClaimedSlot(true);   
            }
            else
            {
                LockUnlockSlot(_locationUtils.GetLvlPassed() < item.NumLvlToUnlock);
            }
            
            UpdateAvalabilityToBuy();
        }

        private void UpdateAvalabilityToBuy()
        {
            buyButton.interactable = _counterUtils.GetMoney(MoneyType.TICKET) >= item.Cost 
                                     && _locationUtils.GetLvlPassed() >= item.NumLvlToUnlock 
                                     && !_shopUtils.IsSkinClaimed(skinType);
        }
        
        protected override void Buy()
        {
            base.Buy();
            _shopUtils.SetSkinClaimed(true, skinType); 
            MessageBroker.Default.Publish(new CounterUpdateEvent()
            {
                Amount = -skinType.GetAttribute<NonPremItem>().Cost,
                MoneyType = MoneyType.TICKET
            });
            EventsInvoker.TriggerEvent("UpdateAvailaibilityToBuy");
        }
    }
}