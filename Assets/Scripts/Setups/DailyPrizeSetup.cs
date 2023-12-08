using System;
using System.Collections.Generic;
using DefaultNamespace.DailyPrizeDialog;
using UnityEngine;

namespace Setups
{
    [CreateAssetMenu(fileName = "DailyPrize", menuName = "Setups/DailyPrizeSetup")]
    public class DailyPrizeSetup : ScriptableObject
    {
        [SerializeField] private List<DailyPrize> _dailyPrizes;

        public List<DailyPrize> GetDailyPrizes() => _dailyPrizes;
    }

    [Serializable]
    public class DailyPrize
    {
        [SerializeField] private DailyPrizeType _dailyPrizeType;
        [SerializeField] private Sprite icon;
        [SerializeField] private int amount;
        [SerializeField] private int dayNum;

        public DailyPrizeType DailyPrizeType
        {
            get => _dailyPrizeType;
            set => _dailyPrizeType = value;
        }

        public Sprite Icon
        {
            get => icon;
            set => icon = value;
        }

        public int Amount
        {
            get => amount;
            set => amount = value;
        }

        public int DayNum
        {
            get => dayNum;
            set => dayNum = value;
        }
    }
}