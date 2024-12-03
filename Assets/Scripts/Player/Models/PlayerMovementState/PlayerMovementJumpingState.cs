using System.Collections;
using UnityEngine;

public class PlayerMovementJumpingState: IPlayerMovementState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="playerData">PlayerData</param>
    public PlayerMovementJumpingState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
        HandleAnimation();
		Debug.Log("Jump State");
    }

    public void HandleMovement()
    {
		if (Player.IsHittingCeiling)
			Player.Velocity.y = 0f;

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
        var horizontalInput = UserInput.Instance.Move.x;
        Player.Velocity.x = horizontalInput * PlayerData.MoveSpeed;
    }

    private void ApplyGravity()
    {
        var gravityScale = UserInput.Instance.IsJumpHeld ? PlayerData.LowGravityScale : PlayerData.DefaultGravityScale;
        Player.Velocity.y += PlayerData.Gravity * gravityScale * Time.deltaTime;
		Player.Velocity.y = Mathf.Min(Player.Velocity.y, PlayerData.MinBlowingSpeed);
    }

	public void HandleAnimation()
    {
        Player.SetAnimation(PlayerStateType.Jump);
	}
}


