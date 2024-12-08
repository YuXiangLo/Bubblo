using UnityEngine;

public class MovementDieState : IMovementState
{
    private Player Player;
    private PlayerData PlayerData;
    private float DieTimer;

    public bool AttackEnabled => false;

    public MovementDieState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
        Player.SetAnimation(AnimationStateType.Die);
        DieTimer = PlayerData.DieClip.length;
    }


    public void Enter()
    {
        Player.Velocity = Vector2.zero;
    }

    public void Update()
    {
        DieTimer -= Time.deltaTime;
        if (DieTimer <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}