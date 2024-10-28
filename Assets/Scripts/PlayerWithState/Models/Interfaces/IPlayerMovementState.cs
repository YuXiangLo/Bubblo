using UnityEngine;
using System.Collections;

public interface IPlayerMovementState
{
    PlayerControl PlayerControl { get; }
    PlayerData PlayerData { get; }

    public void HandleMovement();
}
