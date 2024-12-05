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
    }

    private bool DetectClimbFalling()
    {
        if (!(UserInput.Instance.Move.y < 0.01f))
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