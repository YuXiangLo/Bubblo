using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public AudioSource MainMenuBackGroundMusic;
    public AudioSource InGameBackGroundMusic;
    private AudioSource AudioSource;

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

        AudioSource = MainMenuBackGroundMusic;
        AudioSource.Play();
    }

    public void PlayMainMenuBackgroundMusic()
    {
        if (AudioSource != MainMenuBackGroundMusic)
        {
            if (AudioSource.isPlaying)
            {
                AudioSource.Stop();
            }
            AudioSource = MainMenuBackGroundMusic;
            AudioSource.Play();
        }
    }

    public void PlayInGameBackgroundMusic()
    {
        if (AudioSource != InGameBackGroundMusic)
        {
            if (AudioSource.isPlaying)
            {
                AudioSource.Stop();
            }
            AudioSource = InGameBackGroundMusic;
            AudioSource.Play();
        }
    }
}
