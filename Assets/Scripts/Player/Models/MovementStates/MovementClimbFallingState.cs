using UnityEngine;

public class MovementClimbFallingState : IMovementState
{
    private Player Player;
    private PlayerData Data;

    public MovementClimbFallingState(Player player, PlayerData data)
    {
        Player = player;
        Data = data;
        Player.SetAnimation(AnimationStateType.ClimbFalling);
    }
    public void Enter()
    {
        Debug.Log("ClimbFalling");
        Player.Velocity = new(0f, -Data.ClimbFallingSpeed);
        Update();
    }

    public void Update()
    {
        if (DetectGround() || DetectLeave() || DetectClimb())
        {
            return;
        }
        DetectHorizontalMovement();
    }

    private bool DetectClimb()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Player.ChangeMovementState(new MovementClimbingState(Player, Data));
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

    private bool DetectGround()
    {
        if (Player.Grounded)
        {
            Player.ChangeMovementState(new MovementIdleState(Player, Data));
            return true;
        }
        return false;
    }

    private void DetectHorizontalMovement()
    {
        var horizontalInput = UserInput.Instance.Move.x;
        Player.Velocity = new(horizontalInput * Data.MoveSpeed, Player.Velocity.y);
    }
}