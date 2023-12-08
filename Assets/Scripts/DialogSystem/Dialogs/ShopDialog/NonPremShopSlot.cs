using Counters;
using DefaultNamespace.Locations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.ShopDialog
{
    public class NonPremShopSlot : ShopSlot
    {
        [SerializeField] protected Image claimedButtonIcon;
        [SerializeField] protected Image moneyIcon;
        [SerializeField] protected Image closedSlot;
        [SerializeField] protected TextMeshProUGUI lvlNeed;
        protected LocationUtils _locationUtils;
        protected CounterUtils _counterUtils;
        
        protected override void Start()
        {
            base.Start();
            _locationUtils = new LocationUtils();
            _counterUtils = new CounterUtils();
        }

        protected void LockUnlockSlot(bool isLock)
        {
            buyButton.interactable = !isLock;
            lvlNeed.gameObject.SetActive(isLock);
            closedSlot.gameObject.SetActive(isLock);
        }

        protected void ClaimedSlot(bool isClaimed)
        {
            buyButton.interactable = !isClaimed;
            claimedButtonIcon.gameObject.SetActive(isClaimed);
            moneyIcon.gameObject.SetActive(!isClaimed);
            cost.gameObject.SetActive(!isClaimed);
            LockUnlockSlot(!isClaimed);
        }

        protected override void Buy()
        {
            base.Buy();
            moneyIcon.gameObject.SetActive(false);
            buyButton.interactable = false;
            ClaimedSlot(true);
        }
    }
}