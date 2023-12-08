using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelPassedDialog : Dialog
    {
        [SerializeField] private TextMeshProUGUI rewardText;

        public void Setup(int reward)
        {
            rewardText.text = reward.ToString();
            gameObject.SetActive(true);
        }

        protected override void CloseDialog()
        {
            gameObject.SetActive(false);
        }
    }
}