using UnityEngine;

public class MovementDieState : IMovementState
{
    private Player Player;
    private PlayerData PlayerData;
    private float RemainingTime;

    public MovementDieState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
        Player.SetAnimation(AnimationStateType.Die);
        var stateInfo = Player.Animator.GetCurrentAnimatorStateInfo(0);
        RemainingTime = stateInfo.length * (1f - stateInfo.normalizedTime);
    }

    public void Enter()
    {
        Player.Velocity = Vector2.zero;
    }

    public void Update()
    {
        RemainingTime -= Time.deltaTime;
        if (RemainingTime <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}