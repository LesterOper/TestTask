using System;
using UnityEngine;

namespace DefaultNamespace.Utils
{
    public class MusicAndAudioController : MonoBehaviour
    {
        public static MusicAndAudioController Instance;
        [SerializeField] private AudioSource music;
        [SerializeField] private AudioSource sound;
        [SerializeField] private AudioClip click;
        [SerializeField] private AudioClip ambient;
        private string SOUND = "SOUND";
        private string MUSIC = "MUSIC";
        private void Start()
        {
            PlayMusic();
            OnOffMusic();
            OnOffSound();
            if (Instance == null) Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void SetMusicState()
        {
            bool isOn = PlayerPrefs.GetInt(MUSIC) == 1;
            PlayerPrefs.SetInt(MUSIC, !isOn ? 1 : 0);
            OnOffMusic();
        }
        
        public void SetSoundState()
        {
            bool isOn = PlayerPrefs.GetInt(SOUND) == 1;
            PlayerPrefs.SetInt(SOUND, !isOn ? 1: 0);
            OnOffSound();
        }
        
        private void OnOffMusic()
        {
            bool isOn = PlayerPrefs.GetInt(MUSIC) == 1;
            music.volume = isOn ? 1 : 0;
        }

        private void OnOffSound()
        {
            bool isOn = PlayerPrefs.GetInt(SOUND) == 1;
            sound.volume = isOn ? 1 : 0;
        }

        public void PlaySound()
        {
            sound.Play();
        }

        public void PlayMusic()
        {
            music.loop = true;
            music.Play();
        }
    }
}