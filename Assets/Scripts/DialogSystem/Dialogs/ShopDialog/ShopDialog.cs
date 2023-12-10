using System;
using DefaultNamespace.Locations;
using DefaultNamespace.Utils;
using DefaultNamespace.Utils.IAPController;
using UniRx;
using UnityEngine.Purchasing;

namespace DefaultNamespace.ShopDialog
{
    public class ShopDialog : Dialog
    {
        private InAppPurchasingController _inAppPurchasingController;
        public static Action<Buy> buyChest;
        protected override void Start()
        {
            base.Start();
            _inAppPurchasingController = FindObjectOfType<InAppPurchasingController>();
            buyChest = BuyChest;
            _inAppPurchasingController.Setup();
        }

        private void BuyChest(Buy buy)
        {
            ChestType chestType = buy.ChestType;
            _inAppPurchasingController.SetupCallback(buy.Callback);
            InAppPurchasingController._storeController.InitiatePurchase(chestType.GetAttribute<ChestItem>().Sku);
        }
    }

    public class Buy
    {
        public ChestType ChestType { get; set; }
        public Action Callback { get; set; }
    }
}