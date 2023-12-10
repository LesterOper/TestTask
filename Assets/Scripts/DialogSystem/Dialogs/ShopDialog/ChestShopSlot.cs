using System.Linq;
using Counters;
using DefaultNamespace.Utils;
using DefaultNamespace.Utils.IAPController;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Purchasing;

namespace DefaultNamespace.ShopDialog
{
    public class ChestShopSlot : ShopSlot
    {
        [SerializeField] private ChestType chestType;
        [SerializeField] private TextMeshProUGUI textPrizeCount;
        
         
        protected override void Start()
        {
            base.Start();
            textPrizeCount.text = chestType.GetAttribute<ChestItem>().Amount.ToString();
            cost.text = InAppPurchasingController._storeController.products
                .WithID(chestType.GetAttribute<ChestItem>().Sku).metadata.localizedPriceString + "$";
        }

        protected override void Buy()
        {
            base.Buy();
            
            ShopDialog.buyChest.Invoke(new Buy()
            {
                Callback = CompletePurchase,
                ChestType = chestType
            });
        }

        private void CompletePurchase()
        {
            MessageBroker.Default.Publish(new CounterUpdateEvent()
            {
                Amount = chestType.GetAttribute<ChestItem>().Amount,
                MoneyType = MoneyType.TICKET
            });
            EventsInvoker.TriggerEvent("UpdateAvailaibilityToBuy");
        }
    }
}