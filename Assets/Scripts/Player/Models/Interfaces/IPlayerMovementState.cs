using UnityEngine;
using System.Collections;

public interface IPlayerMovementState
{
    Player Player { get; }
    PlayerData PlayerData { get; }

    public void HandleMovement();
}
