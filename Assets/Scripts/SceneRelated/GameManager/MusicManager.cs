using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    
    // Background Music
    public AudioSource MainMenuBackGroundMusic;
    public AudioSource InGameBackGroundMusic;
    
    // Button
    public AudioSource ButtonPushedSoundEffect;

    private AudioSource BackgroundMusic;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        MainMenuBackGroundMusic.enabled = true;
        InGameBackGroundMusic.enabled = true;
        ButtonPushedSoundEffect.enabled = true;

        BackgroundMusic = MainMenuBackGroundMusic;
        BackgroundMusic.Play();
    }

    public void PauseBackgroundMusic()
    {
        if (BackgroundMusic.isPlaying)
        {
            BackgroundMusic.Pause();
        }
    }

    public void PlayOrResumeBackgroundMusic()
    {
        if (!BackgroundMusic.isPlaying)
        {
            BackgroundMusic.Play();
        }
    }

    public void PlayMainMenuBackgroundMusic()
    {
        if (BackgroundMusic != MainMenuBackGroundMusic)
        {
            if (BackgroundMusic.isPlaying)
            {
                BackgroundMusic.Stop();
            }
            BackgroundMusic = MainMenuBackGroundMusic;
        }

        if (!BackgroundMusic.isPlaying)
        {
            BackgroundMusic.Play();
        }
    }

    public void PlayInGameBackgroundMusic()
    {
        if (BackgroundMusic != InGameBackGroundMusic)
        {
            if (BackgroundMusic.isPlaying)
            {
                BackgroundMusic.Stop();
            }
            BackgroundMusic = InGameBackGroundMusic;
        }

        if (!BackgroundMusic.isPlaying)
        {
            BackgroundMusic.Play();
        }
    }

    public void PlayButtonPushedSoundEffect()
    {
        if (ButtonPushedSoundEffect.isPlaying)
        {
            ButtonPushedSoundEffect.Stop();
        }

        ButtonPushedSoundEffect.Play();
    }
}
