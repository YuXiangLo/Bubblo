using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource MainMenuBackGroundMusic;
    public AudioSource InGameBackGroundMusic;
    

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

        BackgroundMusic = MainMenuBackGroundMusic;
        BackgroundMusic.Play();
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
            BackgroundMusic.Play();
        }
    }
}
