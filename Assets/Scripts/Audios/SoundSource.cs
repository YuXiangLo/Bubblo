using System;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    BackgroundMusic,
    Button,
    Player,
    Bubble,
    Enemy,
}

public enum BackgroundMusicType
{
    // Main Menu BackGroundMusic
    MainMenu,
    // BackGroundMusic while Playing Game
    InGame,
}

public enum ButtonSoundType
{
    Pressed
}

public enum PlayerSoundType
{
    ThrowBubble,
    ChargingBubble,
    WalkOnSoil,
    WalkOnGrass,
    Jump,
    Landing,
    Climbing,
    Attacked,
    Dead,
}

public enum BubbleSoundType
{
    Broken
}

public enum EnemySoundType
{
    AttackSprint,
    AttackJump
}

[CreateAssetMenu(menuName = "SoundSource", fileName = "SoundSourceObject")]
public class SoundSource : ScriptableObject
{
    public SoundList[] SoundLists;

    private void OnEnable()
    {
        if (SoundLists == null)
        {
            return;
        }

        string[] soundTypeNames = Enum.GetNames(typeof(SoundType));
        if (soundTypeNames.Length != SoundLists.Length)
        {
            Array.Resize(ref SoundLists, soundTypeNames.Length);
        }

        for (int i = 0; i < SoundLists.Length; i++)
        {
            SoundLists[i].Name = soundTypeNames[i];
            SoundLists[i].Volume = 1f;
            SetAudioClipName(ref SoundLists[i],(SoundType)i);
        }
    }

    private void SetAudioClipName(ref SoundList soundList, SoundType soundType)
    {
        string[] audioClipNames = Array.Empty<string>();
        switch (soundType)
        {
            case SoundType.BackgroundMusic:
                audioClipNames = Enum.GetNames(typeof(BackgroundMusicType));
                Array.Resize(ref soundList.Sounds, audioClipNames.Length);
                break;
            case SoundType.Button:
                audioClipNames = Enum.GetNames(typeof(ButtonSoundType));
                Array.Resize(ref soundList.Sounds, audioClipNames.Length);
                break;
            case SoundType.Player:
                audioClipNames = Enum.GetNames(typeof(PlayerSoundType));
                Array.Resize(ref soundList.Sounds, audioClipNames.Length);
                break;
            case SoundType.Bubble:
                audioClipNames = Enum.GetNames(typeof(BubbleSoundType));
                Array.Resize(ref soundList.Sounds, audioClipNames.Length);
                break;
            case SoundType.Enemy:
                audioClipNames = Enum.GetNames(typeof(EnemySoundType));
                Array.Resize(ref soundList.Sounds, audioClipNames.Length);
                break;
            default:
                break;
        }

        for (int i = 0; i < audioClipNames.Length; i++)
        {
            soundList.Sounds[i].Name = audioClipNames[i];
        }
    }
}

[Serializable]
public struct SoundList
{
    // Just For Inspector Name
    [HideInInspector] public string Name;
    // Adjust All Sounds' Volume In Same Type
    [Range(0, 1)] public float Volume;
    public Sound[] Sounds;
}

[Serializable]
public struct Sound
{
    [HideInInspector] public string Name;
    public AudioSource AudioSource;
}