using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace DefaultNamespace.Utils.IAPController
{
    public class InAppPurchasingController : MonoBehaviour, IStoreListener
    {
        public static IStoreController _storeController;

        public  IStoreController StoreController => _storeController;

        private IExtensionProvider _extensionProvider;

        private Action _callback;
        
        public void SetupCallback(Action callback)
        {
            _callback = callback;
        }

        private void Start()
        {
            /*var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct("epic_chest", ProductType.Consumable);
            builder.AddProduct("lucky_chest", ProductType.Consumable);
            StandardPurchasingModule.Instance().useFakeStoreAlways = true;
            StandardPurchasingModule.Instance().useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
            UnityPurchasing.Initialize(this, builder);*/
            DontDestroyOnLoad(gameObject);
        }

        public void Setup()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct("epic_chest", ProductType.Consumable);
            builder.AddProduct("lucky_chest", ProductType.Consumable);
            StandardPurchasingModule.Instance().useFakeStoreAlways = true;
            StandardPurchasingModule.Instance().useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogError("ERROR - " + error);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError("ERROR - " + error);
            Debug.LogError("Trace - " + message);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            Debug.Log("PURCHASED " + purchaseEvent.purchasedProduct.definition.id);
            _callback?.Invoke();
            return PurchaseProcessingResult.Complete;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.LogError("ERROR - " + product + " Reason - " + failureReason);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            _extensionProvider = extensions;
        }
    }
}