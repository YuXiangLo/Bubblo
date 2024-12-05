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
        Update();
    }

    public void Update()
    {
        if (DetectLeave() || DetectClimb())
        {
            return;
        }
    }

    private bool DetectClimb()
    {
        if (UserInput.Instance.Move.y > 0.01f)
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
}