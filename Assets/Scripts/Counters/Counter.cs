using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Counters
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private MoneyType _moneyType;
        [SerializeField] private TextMeshProUGUI counterText;
        [SerializeField] private Button debug;
        private CounterUtils _counterUtils;
        private int amount = 0;

        private void Start()
        {
            _counterUtils = new CounterUtils();
            amount = _counterUtils.GetMoney(_moneyType);
            counterText.text = amount.ToString();
            MessageBroker.Default.Receive<CounterUpdateEvent>().Subscribe(AppendCounter);
            debug.OnClickAsObservable().Subscribe(_ => MessageBroker.Default.Publish(new CounterUpdateEvent()
            {
                Amount = 34,
                MoneyType = MoneyType.TICKET
            }));
        }

        private void AppendCounter(CounterUpdateEvent counterUpdateEvent)
        {
            if(counterUpdateEvent.MoneyType != _moneyType) return;
            if(_counterUtils == null) _counterUtils = new CounterUtils();
            
            amount = _counterUtils.AppendMoney(counterUpdateEvent.MoneyType, counterUpdateEvent.Amount);
            counterText.text = amount.ToString();
        }
        
    }

    public enum MoneyType
    {
        NONE = 0,
        TICKET = 1,
        REAL = 2,
    }

    public class CounterUtils
    {
        private string MONEY_PREF = "Money_";

        public int GetMoney(MoneyType moneyType) => PlayerPrefs.GetInt(MONEY_PREF + moneyType);

        private void SetMoney(MoneyType moneyType, int amount = 0)
        {
            PlayerPrefs.SetInt(MONEY_PREF + moneyType, amount <= 0 ? 0 : amount);
        }

        public int AppendMoney(MoneyType moneyType, int append = 0)
        {
            int amount = PlayerPrefs.GetInt(MONEY_PREF + moneyType);
            amount += append;
            SetMoney(moneyType, amount);
            return amount;
        }
    }
}