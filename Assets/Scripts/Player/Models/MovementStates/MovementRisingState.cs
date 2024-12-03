using UnityEngine;

public class MovementRisingState : IMovementState
{
    private readonly Player Player;
    private readonly PlayerData Data;
    public MovementRisingState(Player player, PlayerData data)
    {
        Player = player;
        Data = data;
    }
    
    public void Enter()
    {
        // Do nothing. Set Jump force or Bubble jump force before change state
    }

    public void Update()
    {
        if (Player.HitCeiling)
        {
            Player.Velocity = new(Player.Velocity.x, 0f);
        }

        DetectHorizontalMovement();
        ApplyGravity();
        
        if (Player.Velocity.y <= 0)
        {
            Player.ChangeMovementState(new MovementFallingState(Player, Data));
            return;
        }
    }

    private void DetectHorizontalMovement()
    {
        var horizontalInput = UserInput.Instance.Move.x - UserInput.Instance.Move.y;
        Player.Velocity = new(horizontalInput * Data.MoveSpeed, Player.Velocity.y);
    }

    private void ApplyGravity()
    {
        var gravityScale = UserInput.Instance.IsJumpHeld ? Data.LowGravityScale : Data.DefaultGravityScale;
        var velocity = Player.Velocity;
        velocity.y += Data.Gravity * gravityScale * Time.deltaTime;
        velocity.y = Mathf.Min(velocity.y, Data.MinBlowingSpeed);
        Player.Velocity = velocity;
    }
}