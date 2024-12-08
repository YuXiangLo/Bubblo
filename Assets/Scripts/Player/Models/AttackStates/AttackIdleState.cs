using UnityEngine;

public class AttackIdleState : IAttackState
{
    private Player Player;
    private PlayerData PlayerData;
    public bool LockAnimation => false;

    private bool exit = false;

    public AttackIdleState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
    }

    public void Update()
    {
        if (exit)
        {
            return;
        }
        if (Player.AttackEnabled is false)
        {
            return;
        }
        if (UserInput.Instance.FireKeyDown)
        {
            exit = true;
            if (Player.HoldingBubble)
            {
                Player.HoldingBubble.StopCharging();
                Player.HoldingBubble.Release();
            }
            Player.HoldingBubble = Player.InitializeBubble();
            Player.ChangeAttackState(new AttackChargingState(Player, PlayerData, Player.HoldingBubble));
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