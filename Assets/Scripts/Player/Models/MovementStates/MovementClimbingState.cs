using UnityEngine;

public class MovementClimbingState : IMovementState
{
    private Player Player;
    private PlayerData Data;

    public MovementClimbingState(Player player, PlayerData data)
    {
        Player = player;
        Data = data;
    }
    public void Enter()
    {
        Player.SetAnimation(AnimationStateType.Climbing);
        Player.Velocity = new(0f, Data.ClimbingSpeed);
    }

    public void Update()
    {
        if (DetectLeave() || DetectClimbFalling())
        {
            return;
        }
    }

    private bool DetectClimbFalling()
    {
        if (!Input.GetKey(KeyCode.W))
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
}