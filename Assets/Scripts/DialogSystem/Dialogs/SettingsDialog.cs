using DefaultNamespace.Utils;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class SettingsDialog : Dialog
    {
        [SerializeField] private Button music;
        [SerializeField] private Button sound;

        protected override void Start()
        {
            base.Start();
            music.onClick.AddListener(MusicControl);
            sound.onClick.AddListener(SoundControl);
        }

        private void MusicControl()
        {
            MusicAndAudioController.Instance.SetMusicState();
        }

        private void SoundControl()
        {
            MusicAndAudioController.Instance.SetSoundState();
        }
    }
}