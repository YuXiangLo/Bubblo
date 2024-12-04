using UnityEngine;

public class MovementRisingState : IMovementState
{
    private readonly Player Player;
    private readonly PlayerData Data;
    public MovementRisingState(Player player, PlayerData data)
    {
        Player = player;
        Data = data;
        Player.SetAnimation(AnimationStateType.Rising);
    }
    
    public void Enter()
    {
        // Don't set Jump force or Bubble jump force cause it has been set before change state
        Update();
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
        var horizontalInput = UserInput.Instance.Move.x;
        Player.Velocity = new(horizontalInput * Data.MoveSpeed, Player.Velocity.y);
    }

    private void ApplyGravity()
    {
        var gravityScale = Input.GetKey(KeyCode.Space) ? Data.LowGravityScale : Data.DefaultGravityScale;
        var velocity = Player.Velocity;
        velocity.y += Data.Gravity * gravityScale * Time.deltaTime;
        velocity.y = Mathf.Min(velocity.y, Data.MinBlowingSpeed);
        Player.Velocity = velocity;
    }
}