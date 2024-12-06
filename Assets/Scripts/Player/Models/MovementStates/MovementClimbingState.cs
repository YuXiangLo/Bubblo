using UnityEngine;

public class MovementClimbingState : IMovementState
{
    private Player Player;
    private PlayerData Data;

    public MovementClimbingState(Player player, PlayerData data)
    {
        Player = player;
        Data = data;
        Player.SetAnimation(AnimationStateType.Climbing);
    }
    public void Enter()
    {
        Player.Velocity = new(0f, Data.ClimbingSpeed);
        Update();
    }

    public void Update()
    {
        if (DetectLeave() || DetectClimbFalling())
        {
            return;
        }
        DetectHorizontalMovement();
    }

    private bool DetectClimbFalling()
    {
        if (UserInput.Instance.IsUpHeld is false)
        {
            Player.ChangeMovementState(new MovementClimbFallingState(Player, Data));
            return true;
        }
        return false;
    }

    private bool DetectLeave()
    {
        if (!Player.IsAbleToClimb)
        {
            Player.ChangeMovementState(new MovementInitialState(Player, Data));
            return true;
        }
        return false;
    }

    private void DetectHorizontalMovement()
    {
        var horizontalInput = UserInput.Instance.HorizontalInput;
        Player.Velocity = new(horizontalInput * Data.MoveSpeed, Player.Velocity.y);
    }
}