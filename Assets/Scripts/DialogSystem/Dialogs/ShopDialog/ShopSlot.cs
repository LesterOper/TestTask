using System;
using Counters;
using DefaultNamespace.Utils;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.ShopDialog
{
    public abstract class ShopSlot : MonoBehaviour
    {
        [SerializeField] private MoneyType real;
        [SerializeField] protected Button buyButton;
        [SerializeField] protected TextMeshProUGUI cost;
        protected Color notEnoughMoney = Color.red;
        protected ShopUtils _shopUtils;

        protected virtual void Start()
        {
            _shopUtils = new ShopUtils();
            buyButton.onClick.AddListener(() =>
            {
                MusicAndAudioController.Instance.PlaySound();
                Buy();
            });
        }

        protected virtual void Buy(){}
    }

    public class ShopUtils
    {
        private string LOCATION_CLAIMED = "LOCATION_CLAIMED_";
        private string SKIN_CLAIMED = "SKIN_CLAIMED_";

        public bool IsLocationClaimed(LocationType locationType) => PlayerPrefs.GetInt(LOCATION_CLAIMED + locationType) == 1;

        public void SetLocationClaimed(bool isClaimed, LocationType locationType) =>
            PlayerPrefs.SetInt(LOCATION_CLAIMED + locationType, isClaimed ? 1 : 0);
        
        public bool IsSkinClaimed(SkinType skinType) => PlayerPrefs.GetInt(SKIN_CLAIMED + skinType) == 1;

        public void SetSkinClaimed(bool isClaimed, SkinType skin) =>
            PlayerPrefs.SetInt(SKIN_CLAIMED + skin, isClaimed ? 1 : 0);
        
        
    }

    public enum LocationType
    {
        NONE = 0,
        [NonPremItem(MoneyType.TICKET, 100, 0)]
        LOCATION_1 = 1,
        [NonPremItem(MoneyType.TICKET, 200, 0)]
        LOCATION_2 = 2,
        [NonPremItem(MoneyType.TICKET, 300, 10)]
        LOCATION_3 = 3,
    }

    public enum SkinType
    {
        NONE = 0,
        [NonPremItem(MoneyType.TICKET, 100, 0)]
        SKIN_1 = 1,
        [NonPremItem(MoneyType.TICKET, 300, 7)]
        SKIN_2 = 2,
    }

    public enum ChestType
    {
        NONE=0,
        [ChestItem(MoneyType.REAL, "epic_chest", 500)]
        EPIC_CHEST = 1,
        [ChestItem(MoneyType.REAL, "lucky_chest", 1000)]
        LUCKY_CHEST = 2,
    }

    public class NonPremItem : Attribute
    {
        private MoneyType _moneyType;
        private int _cost;
        private int _numLvlToUnlock;

        public int NumLvlToUnlock
        {
            get => _numLvlToUnlock;
            set => _numLvlToUnlock = value;
        }

        public MoneyType MoneyType
        {
            get => _moneyType;
            set => _moneyType = value;
        }

        public int Cost
        {
            get => _cost;
            set => _cost = value;
        }

        public NonPremItem(MoneyType moneyType, int cost, int numLvlToUnlock)
        {
            _moneyType = moneyType;
            _cost = cost;
            _numLvlToUnlock = numLvlToUnlock;
        }
    }
    

    public class ChestItem : Attribute
    {
        private MoneyType _moneyType;
        private string _sku;
        private int _amount;

        public MoneyType MoneyType
        {
            get => _moneyType;
            set => _moneyType = value;
        }

        public string Sku
        {
            get => _sku;
            set => _sku = value;
        }

        public int Amount
        {
            get => _amount;
            set => _amount = value;
        }

        public ChestItem(MoneyType moneyType, string sku, int amount)
        {
            _moneyType = moneyType;
            _sku = sku;
            _amount = amount;
        }
    }
}