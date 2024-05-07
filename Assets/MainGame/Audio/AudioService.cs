using System;
using System.Collections.Generic;
using MainGame.Services.Interfaces;
using MainGame.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MainGame.Services
{

    public class AudioService : Singleton<AudioService>, IAudioService
    {
        public static IAudioService instance;


        [Header("Audio Sources")] [SerializeField]
        private AudioSource bgMusic;

        [SerializeField] private AudioSource soundFX;

        [Header("SoundFX")] [SerializeField] List<AudioClip> soundClips;

        [Header("BGMusic")] [SerializeField] List<AudioClip> musicClips;

        protected override void Initial()
        {
            instance = InstancePrivate;
        }

        public void PlayMusic(MusicClipData clipData)
        {
            bgMusic.clip = musicClips[(int)clipData];
            bgMusic.Play();
        }

        public void PauseMusic()
        {
            bgMusic.Pause();
        }

        public void PlayMusic()
        {
            bgMusic.Play();
        }
        
        [Button]
        public void SetMuteMusic(bool isMute)
        {
            bgMusic.mute                   = isMute;
        }

        public void PlaySfx(SoundFXData soundFXData)
        {
            soundFX.PlayOneShot(soundClips[(int)soundFXData]);
        }

        public void SetMuteSfx(bool isMute)
        {
            soundFX.mute                 = isMute;
        }

        public void StopMusic()
        {
            bgMusic.Stop();
        }
        
    }


    public enum SoundFXData
    {
        Swing1,
        Swing2,
        Deflect1,
        Deflect2,
        Deflect3,
    }

    public enum MusicClipData
    {
        Lobby,
        InGame1,
        InGame2,
        WaitResult,
        InGameClockTick,
        InGameChristmas,
        LobbyNoel,
        LobbyHalloween
    }
}