using System;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    [SerializeField] private float MaxMagic = 10f;
    private float CurrentMagic;
    public float MagicPercentage => CurrentMagic / MaxMagic;
    public bool IsEmpty => CurrentMagic == 0f;

    public void Consume(float amount)
    {
        CurrentMagic = Mathf.Clamp(CurrentMagic - amount, 0f, MaxMagic);
    }

    public void Recharge(float amount)
    {
        CurrentMagic = Mathf.Clamp(CurrentMagic + amount, 0f, MaxMagic);
    }

    public void Initialize() => CurrentMagic = MaxMagic;
    public void Initialize(float magic) => CurrentMagic = magic;
}