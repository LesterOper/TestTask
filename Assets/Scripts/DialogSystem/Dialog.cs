using System;
using DefaultNamespace.Utils;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Dialog : MonoBehaviour
    {
        [SerializeField] private Button closeArea;
        [SerializeField] private Button closeBtn;

        protected virtual void Start()
        {
            if (closeArea != null)
            {
                closeArea.OnClickAsObservable().Subscribe(_ =>
                {
                    MusicAndAudioController.Instance.PlaySound();
                    CloseDialog();
                });
            }

            if (closeBtn != null)
            {
                closeBtn.OnClickAsObservable().Subscribe(_ =>
                {
                    MusicAndAudioController.Instance.PlaySound();
                    CloseDialog();
                });
            }
        }

        protected virtual void CloseDialog()
        {
            DestroyImmediate(gameObject);
        }
    }
}