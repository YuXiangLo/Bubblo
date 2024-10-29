using System.Collections;
using UnityEngine;

public class PlayerMovementJumpingState: IPlayerMovementState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }

    public PlayerMovementJumpingState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
    }

    public void HandleMovement()
    {
        if (Player.Velocity.y > 0f)
        {
            DetectHorizontalMovement();
            ApplyGravity();
        }
        else
        {
            Player.ChangePlayerMovementState(new PlayerMovementFallingState(Player, PlayerData));
        }
    }

    private void DetectHorizontalMovement()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        Player.Velocity.x = horizontalInput * PlayerData.MoveSpeed;
    }

    private void ApplyGravity()
    {
        var gravityScale = Input.GetButton("Jump") ? PlayerData.LowGravityScale : PlayerData.DefaultGravityScale;
        Player.Velocity.y += PlayerData.Gravity * gravityScale * Time.deltaTime;
    }
    
}

