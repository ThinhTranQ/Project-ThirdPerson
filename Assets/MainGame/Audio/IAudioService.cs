namespace MainGame.Services.Interfaces
{
    public interface IAudioService
    {
        void PlayMusic(MusicClipData clipData);
        void PauseMusic();
        void PlayMusic();
        void SetMuteMusic(bool isMute);
        void PlaySfx(SoundFXData soundFXData);
        void SetMuteSfx(bool isMute);
        void StopMusic();
    }
}