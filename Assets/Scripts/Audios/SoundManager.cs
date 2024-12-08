using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private SoundSource SoundSourceObject;
    private static BackgroundMusicType CurrentBackgroundMusicType;

    private SoundSource SoundSource;
    private static float SystemVolumeRatio = 0.5f;
    public bool IsMuted = false;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        ActiveAllAudioSources();

        CurrentBackgroundMusicType = BackgroundMusicType.MainMenu;
        SoundSource
            .SoundLists[(int)SoundType.BackgroundMusic]
            .Sounds[(int)CurrentBackgroundMusicType]
            .AudioSource
            .Play();
    }

    public static float GetSystemVolumeRatio() => SystemVolumeRatio;

    public static void SetSystemVolumeRatio(float volumeRatio)
    {
        SystemVolumeRatio = volumeRatio;
        Instance   
            .SoundSource
            .SoundLists[(int)SoundType.BackgroundMusic]
            .Sounds[(int)CurrentBackgroundMusicType]
            .AudioSource
            .volume = SystemVolumeRatio;
    } 

    public void Mute()
    {
        IsMuted = !IsMuted;
    }

    /// <summary>
    /// Change the backgroundMusic
    /// </summary>
    /// <param name="backgroundMusicType">background music type</param>
    public static void ChangeBackgroundMusic(BackgroundMusicType backgroundMusicType)
    {
        if (CurrentBackgroundMusicType != backgroundMusicType)
        {
            var soundList = Instance
                                .SoundSource
                                .SoundLists[(int)SoundType.BackgroundMusic];
            
            soundList
                .Sounds[(int)CurrentBackgroundMusicType]
                .AudioSource
                .Stop();
            
            var newBackgroundMusic = soundList
                                        .Sounds[(int)backgroundMusicType]
                                        .AudioSource;

            newBackgroundMusic.volume = SystemVolumeRatio;
            newBackgroundMusic.Play();
            
            CurrentBackgroundMusicType = backgroundMusicType;
        }
    }
    
    /// <summary>
    /// Play the sound effect
    /// soundType is the type of the sound (e.g. Player, Enemy)
    /// listNumber should be sent by corresponding enum type. (e.g. (int)PlayerSoundType.Jump)
    /// you can set volume from 1 (100%) to 0 (0%), 1 on default.
    /// </summary>
    /// <param name="soundType">sound type</param>
    /// <param name="listNumber">sound source enum number</param>
    /// <param name="volume">volume(0~1)</param>
    public static void PlaySound(SoundType soundType, int listNumber, float volume = 1)
    {
        AudioSource sound = Instance
                                .SoundSource
                                .SoundLists[(int)soundType]
                                .Sounds[listNumber]
                                .AudioSource;
        
        sound.volume = SystemVolumeRatio * volume;
        if (!sound.isPlaying)
        {
            sound.Play();
        }
    }

    /// <summary>
    /// Stop the sound effect
    /// </summary>
    /// <param name="soundType">sound type</param>
    /// <param name="listNumber">sound source enum number</param>
    public static void StopSound(SoundType soundType, int listNumber)
    {
        AudioSource sound = Instance
                                .SoundSource
                                .SoundLists[(int)soundType]
                                .Sounds[listNumber]
                                .AudioSource;
        
        if (sound.isPlaying)
        {
            sound.Stop();
        }
    }

    private void ActiveAllAudioSources()
    {
        SoundSource = Instantiate(SoundSourceObject);
        for (int i = 0; i < SoundSource.SoundLists.Length; i++)
        {
            for (int j = 0; j < SoundSource.SoundLists[i].Sounds.Length; j++)
            {
                if (SoundSource.SoundLists[i].Sounds[j].AudioSource != null)
                {
                    AudioSource audioInstance = SoundSource.SoundLists[i].Sounds[j].AudioSource;
                    AudioSource instantiateInstance = Instantiate(audioInstance);
                    SoundSource.SoundLists[i].Sounds[j].AudioSource = instantiateInstance;
                    DontDestroyOnLoad(instantiateInstance);
                    SoundSource.SoundLists[i].Sounds[j].AudioSource.gameObject.SetActive(true);
                }
            }
        }
    }
}
