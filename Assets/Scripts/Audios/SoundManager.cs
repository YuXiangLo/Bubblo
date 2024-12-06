using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private SoundSource SoundSourceObject;
    [SerializeField] private BackgroundMusicType CurrentBackgroundMusicType;

    private SoundSource SoundSource;

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

    /// <summary>
    /// Change the backgroundMusic
    /// </summary>
    /// <param name="backgroundMusicType">background music type</param>
    public static void ChangeBackgroundMusic(BackgroundMusicType backgroundMusicType)
    {
        if (Instance.CurrentBackgroundMusicType != backgroundMusicType)
        {
            var soundList = Instance
                                .SoundSource
                                .SoundLists[(int)SoundType.BackgroundMusic];
            
            soundList
                .Sounds[(int)Instance.CurrentBackgroundMusicType]
                .AudioSource
                .Stop();
            
            soundList
                .Sounds[(int)backgroundMusicType]
                .AudioSource
                .Play();
            
            Instance.CurrentBackgroundMusicType = backgroundMusicType;
        }
    }
    
    /// <summary>
    /// Play the sound effect
    /// SoundType is the type of the sound (e.g. Player, Enemy)
    /// ListNumber should be sent by corresponding enum type. (e.g. (int)PlayerSoundType.Jump)
    /// </summary>
    /// <param name="soundType">sound type</param>
    /// <param name="ListNumber">sound source enum number</param>
    public static void PlaySound(SoundType soundType, int ListNumber)
    {
        Instance
            .SoundSource
            .SoundLists[(int)soundType]
            .Sounds[ListNumber]
            .AudioSource.Play();
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
