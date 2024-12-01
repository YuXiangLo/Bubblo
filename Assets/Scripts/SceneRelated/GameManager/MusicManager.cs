using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    
    [Header("BackgroundMusic")]
    public AudioSource MainMenuBackgroundMusic;
    public AudioSource InGameBackgroundMusic;
    
    [Header("Button")]
    public AudioSource ButtonPushedSoundEffect;

    [Header("Player")]
    public AudioSource ThrowBubbleSoundEffect;
    public AudioSource CharingBubbleSoundEffect;
    public AudioSource WalkSoundEffect;
    public AudioSource JumpSoundEffect;
    public AudioSource DeadSoundEffect;
    
    [Header("Bubble")]
    public AudioSource BubbleBrokenSoundEffect;

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

        MainMenuBackgroundMusic.enabled = true;
        InGameBackgroundMusic.enabled = true;
        ButtonPushedSoundEffect.enabled = true;

        BackgroundMusic = MainMenuBackgroundMusic;
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
        if (BackgroundMusic != MainMenuBackgroundMusic)
        {
            if (BackgroundMusic.isPlaying)
            {
                BackgroundMusic.Stop();
            }
            BackgroundMusic = MainMenuBackgroundMusic;
        }

        if (!BackgroundMusic.isPlaying)
        {
            BackgroundMusic.Play();
        }
    }

    public void PlayInGameBackgroundMusic()
    {
        if (BackgroundMusic != InGameBackgroundMusic)
        {
            if (BackgroundMusic.isPlaying)
            {
                BackgroundMusic.Stop();
            }
            BackgroundMusic = InGameBackgroundMusic;
        }

        if (!BackgroundMusic.isPlaying)
        {
            BackgroundMusic.Play();
        }
    }
}
