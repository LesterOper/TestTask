using System;
using DefaultNamespace.DialogEvents;
using UniRx;
using UnityEngine;

namespace DefaultNamespace
{
    public class DialogContainer : MonoBehaviour
    {
        [SerializeField] private SettingsDialog _settingsDialog;
        [SerializeField] private DailyPrizeDialog.DailyPrizeDialog _dailyPrizeDialog;
        [SerializeField] private ShopDialog.ShopDialog _shopDialog;
        [SerializeField] private LevelDialog.LevelDialog _levelDialog;

        private void Start()
        {
            SubscribeDialogsOpenEvent();
            DontDestroyOnLoad(gameObject);
        }

        private void SubscribeDialogsOpenEvent()
        {
            MessageBroker.Default.Receive<DialogOpenEvent>().Subscribe(ShowDialog);
        }

        private void ShowDialog(DialogOpenEvent dialogOpenEvent)
        {
            DialogType dialogType = dialogOpenEvent.DialogType;
            Transform parent = dialogOpenEvent.Parent;

            switch (dialogType)
            {
                case DialogType.SETTINGS :
                    ShowDialog(_settingsDialog, parent);
                    break;
                case DialogType.DAILY_PRIZE :
                    ShowDialog(_dailyPrizeDialog, parent);
                    break;
                case DialogType.SHOP :
                    ShowDialog(_shopDialog, parent);
                    break;
                case DialogType.LEVEL :
                    ShowDialog(_levelDialog, parent);
                    break;
            }
        }

        private void ShowDialog(Dialog dialog, Transform parent)
        {
            Instantiate(dialog.gameObject, parent);
        }
    }
}