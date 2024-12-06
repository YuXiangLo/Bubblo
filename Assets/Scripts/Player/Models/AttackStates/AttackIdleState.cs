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
        if (Player.IsDead) return;
        if (UserInput.Instance.FireKeyDown)
        {
            Player.ChangeAttackState(new AttackChargingState(Player, PlayerData, Player.InitializeBubble()));
        }
    }

    public void Enter()
    {
        // Do nothing
        Update();
    }

    public void Knockbacked()
    {
        // Do nothing
    }
}