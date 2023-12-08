using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.DailyPrizeDialog;
using DefaultNamespace.DialogEvents;
using DefaultNamespace.Utils;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    [SerializeField] private Transform dialogParent;
    [SerializeField] private Button play;
    [SerializeField] private Button settings;
    [SerializeField] private Button gift;
    [SerializeField] private Button shop;
    [SerializeField] private DailyPrizeIndicatorController indicatorDailyPrize;
    private DailyPrizeUtils _dailyPrizeUtils;
    private void Start()
    {
        _dailyPrizeUtils = new DailyPrizeUtils();
        settings.OnClickAsObservable().Subscribe(_ =>
        {
            MusicAndAudioController.Instance.PlaySound();

            MessageBroker.Default.Publish(new DialogOpenEvent()
            {
                DialogType = DialogType.SETTINGS,
                Parent = dialogParent
            });
        });
        gift.OnClickAsObservable().Subscribe(_ =>
        {
            MusicAndAudioController.Instance.PlaySound();

            MessageBroker.Default.Publish(new DialogOpenEvent()
            {
                DialogType = DialogType.DAILY_PRIZE,
                Parent = dialogParent,
            });
        });
        shop.OnClickAsObservable().Subscribe(_ =>
        {
            MusicAndAudioController.Instance.PlaySound();
            MessageBroker.Default.Publish(new DialogOpenEvent()
            {
                DialogType = DialogType.SHOP,
                Parent = dialogParent,
            });
        });
        
        play.OnClickAsObservable().Subscribe(_ =>
        {
            MusicAndAudioController.Instance.PlaySound();

            MessageBroker.Default.Publish(new DialogOpenEvent()
            {
                DialogType = DialogType.LEVEL,
                Parent = dialogParent,
            });
        });
        if (_dailyPrizeUtils.GetCountClaimedDailyPrize() == 0 )
        {
            SetFirstDayPrize();
            indicatorDailyPrize.ActivateIndicator();
        }
        else CheckDailyPrize();
    }

    private void SetFirstDayPrize()
    {
        _dailyPrizeUtils.SetHasPrize(true);
        _dailyPrizeUtils.SetLastDayClaimed(0);
        _dailyPrizeUtils.SetCurrentDailyPrize(1);
    }

    private void CheckDailyPrize()
    {
        long lastClaimedTicks = 0;
        if(long.TryParse(_dailyPrizeUtils.GetLastDailyPrizeClaimed(), out lastClaimedTicks))
        {
            long buf = DateTime.UtcNow.Ticks - lastClaimedTicks;

            if (buf >= TimeSpan.TicksPerDay)
            {
                if (_dailyPrizeUtils.GetLastDayClaimed() >= 7)
                {
                    _dailyPrizeUtils.ResetDailyPrize();
                    SetFirstDayPrize();
                }
                else
                {
                    _dailyPrizeUtils.SetHasPrize(true);
                    _dailyPrizeUtils.SetCurrentDailyPrize(_dailyPrizeUtils.GetCurrentDailyPrize() + 1);
                }

                indicatorDailyPrize.ActivateIndicator();
            }
            else indicatorDailyPrize.DeactivateIndicator();
        }
    }
}
