using UnityEngine;

public class AttackIdleState : IAttackState
{
    private Player Player;
    private PlayerData PlayerData;
    public bool LockAnimation => false;

    public AttackIdleState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
    }

    public void Update()
    {
        if (UserInput.Instance.Fire)
        {
            if (Player.IsMagicEmpty)
            {
                // Current Magic Point > 0
                Player.ChangeAttackState(new AttackPitchingState(Player, PlayerData, Player.InitializeBubble()));
            }
            else
            {
                Player.ChangeAttackState(new AttackChargingState(Player, PlayerData, Player.InitializeBubble()));
            }
        }
    }

    public void Enter()
    {
        // Do nothing
    }

    public void Knockbacked()
    {
        // Do nothing
    }
}