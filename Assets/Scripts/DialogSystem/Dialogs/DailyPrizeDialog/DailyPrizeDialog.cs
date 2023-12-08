using System;
using System.Collections;
using System.Globalization;
using Counters;
using Setups;
using UniRx;
using UnityEngine;

namespace DefaultNamespace.DailyPrizeDialog
{
    public class DailyPrizeDialog : Dialog
    {
        [SerializeField] private DailyPrizeSetup dailyPrizeSetup;
        [SerializeField] private DailyPrizeSlot slotPrefab;
        [SerializeField] private DialyPrizeProgress _dialyPrizeProgress;
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject day7;
        [SerializeField] private GameObject dialogMain;

        private Action _weekEndEvent;
        private DailyPrizeUtils _dailyPrizeUtils;

        protected override void Start()
        {
            base.Start();
            _dailyPrizeUtils = new DailyPrizeUtils();
            _weekEndEvent += InvokeWeekEndEvent;
            StartCoroutine(CreatePrizes());
            _dialyPrizeProgress.Setup(_weekEndEvent);
        }

        private IEnumerator CreatePrizes()
        {
            var prizes = dailyPrizeSetup.GetDailyPrizes();
            
            for (int i = 0; i < prizes.Count-1; i++)
            {
                var dailyPrizeSlot = Instantiate(slotPrefab, parent);
                bool isCLickable = _dailyPrizeUtils.HasPrize() && _dailyPrizeUtils.GetCurrentDailyPrize() == prizes[i].DayNum;
                var buf = _dailyPrizeUtils.GetLastDayClaimed();
                var buf2 = buf == prizes[i].DayNum;
                bool isAlreadyClaimed = _dailyPrizeUtils.GetLastDayClaimed() >= prizes[i].DayNum;
                dailyPrizeSlot.Setup(prizes[i], prizes[i].DayNum, isCLickable, isAlreadyClaimed);

                yield return new WaitForSeconds(0.5f);
            }

            yield return null;
        }

        private void InvokeWeekEndEvent()
        {
            Debug.Log("WEEK END");
            day7.SetActive(true);
            dialogMain.SetActive(false);
            MessageBroker.Default.Publish(new CounterUpdateEvent()
            {
                Amount = dailyPrizeSetup.GetDailyPrizes()[6].Amount,
                MoneyType = MoneyType.TICKET
            });
        }
    }

    public class DailyPrizeUtils
    {
        private string CURRENT_DAILY_PRIZE = "CURRENT_DAILY_PRIZE";
        private string LAST_DAILY_PRIZE_CLAIMED = "LAST_DAILY_PRIZE_CLAIMED";
        private string HAS_PRIZE = "HAS_PRIZE";
        private string COUNT_CLAIMED_DAILY_PRIZE = "COUNT_CLAIMED_DAILY_PRIZE";
        private string CLAIMED_DAILY_PRIZE = "CLAIMED_DAILY_PRIZE";

        public void SetCurrentDailyPrize(int day) => PlayerPrefs.SetInt(CURRENT_DAILY_PRIZE, day);

        public int GetCurrentDailyPrize() => PlayerPrefs.GetInt(CURRENT_DAILY_PRIZE);

        public void SetLastDailyPrizeClaimed(long ticks) =>
            PlayerPrefs.SetString(LAST_DAILY_PRIZE_CLAIMED, ticks.ToString());

        public string GetLastDailyPrizeClaimed() => PlayerPrefs.GetString(LAST_DAILY_PRIZE_CLAIMED);

        public void SetHasPrize(bool has) => PlayerPrefs.SetInt(HAS_PRIZE, has ? 1 : 0);
        public bool HasPrize() => PlayerPrefs.GetInt(HAS_PRIZE) == 1;

        public void SetCountClaimedDailyPrize(int count) => PlayerPrefs.SetInt(COUNT_CLAIMED_DAILY_PRIZE, count);
        public int GetCountClaimedDailyPrize() => PlayerPrefs.GetInt(COUNT_CLAIMED_DAILY_PRIZE);

        public int GetLastDayClaimed() => PlayerPrefs.GetInt(CLAIMED_DAILY_PRIZE);
        public void SetLastDayClaimed(int day) => PlayerPrefs.SetInt(CLAIMED_DAILY_PRIZE, day);

        public void ResetDailyPrize()
        {
            SetCountClaimedDailyPrize(0);
            SetLastDayClaimed(0);
            SetCurrentDailyPrize(1);
        }
    }
}