using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    public AudioClip fall;
    public AudioClip error;
    public AudioClip crash;
    public AudioClip boost;
    public AudioClip supplies;
    public AudioClip burning; 
    public AudioClip sticky;
    public AudioClip mainmenu;
    public AudioClip play;
    public AudioClip gameover;

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float speed)
    {
        sfxSource.pitch = speed;
        sfxSource.PlayOneShot(clip);
    }
    public void UnPauseMusic()
    {
        musicSource.UnPause();
    }

    public void UnPauseSFX()
    {
        sfxSource.UnPause();
    }
    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void PauseSFX()
    {
        sfxSource.Pause();
    }

    public void StopMusic()
    {
            musicSource.Stop();
    }
    public void StopSFX()
    {
        sfxSource.Stop();
    }

}
