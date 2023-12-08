using System;
using Counters;
using DefaultNamespace.Locations;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.LevelDialog
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Button lvlBtnComp;
        [SerializeField] private GameObject passed;
        [SerializeField] private TextMeshProUGUI lvlNum;
        private LocationUtils _locationUtils;
        private Action<int> _callback;
        private int _reward;
        private int _lvl;
        
        public void Setup(int lvl, int reward, Action<int> callback)
        {
            lvlBtnComp.onClick.AddListener(LvlPassed);
            _locationUtils = new LocationUtils();
            SetLvlProps(lvl, reward);
            _callback = callback;
            
            bool isPassed = _locationUtils.GetLvlPassed() >= lvl;
            UpdateLevelView(isPassed);
        }

        private void SetLvlProps(int lvl, int reward)
        {
            _lvl = lvl;
            _reward = reward;
        }
        
        private void UpdateLevelView(bool isPassed)
        {
            lvlBtnComp.interactable = !isPassed;
            passed.SetActive(isPassed);
            
            lvlNum.gameObject.SetActive(!isPassed);
            lvlNum.text = _lvl.ToString();
        }

        private void LvlPassed()
        {
            lvlNum.gameObject.SetActive(false);
            _locationUtils.SetLvlPassed(_lvl);
            lvlBtnComp.interactable = false;
            passed.SetActive(true);
            _callback?.Invoke(_reward);
            MessageBroker.Default.Publish(new CounterUpdateEvent()
            {
                Amount = _reward,
                MoneyType = MoneyType.TICKET
            });
        }
    }
}