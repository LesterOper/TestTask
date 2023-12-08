using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.LevelDialog
{
    public class LevelDialog : Dialog
    {
        [SerializeField] private List<LevelPartController> _levelPartControllers;
        [SerializeField] private LevelPassedDialog _levelPassedDialog;
        private int defaultReward = 5;

        protected override void Start()
        {
            base.Start();
            
            CalculateLvlInfo();
        }

        private void CalculateLvlInfo()
        {
            int index = 0;
            for (int i = 0; i < _levelPartControllers.Count; i++)
            {
                index = _levelPartControllers[i].Setup(index , defaultReward, ShowLevelPassedDialog);
            }
        }

        private void ShowLevelPassedDialog(int reward)
        {
            _levelPassedDialog.Setup(reward);
        }
    }
}