using UnityEngine;
public class AttackChargingState : IAttackState
{
    private Player Player;
    private PlayerData PlayerData;
    private Bubble Bubble;
    public bool LockAnimation => true;

    private bool StopCharge = false;
    private bool exit = false;

    public AttackChargingState(Player player, PlayerData playerData, Bubble bubble)
    {
        Player = player;
        PlayerData = playerData;
        Bubble = bubble;
    }

    public void Enter()
    {
        // Do nothing
        Player.SetAnimation(AnimationStateType.Charging);
        if (Player.IsMagicEmpty)
        {
            Bubble.StopCharging();
            ReleaseBubble();
        }
    }

    public void Knockbacked()
    {
        Bubble.Burst();
        Player.ChangeAttackState(new AttackIdleState(Player, PlayerData));
    }

    public void Update()
    {
        if (exit)
        {
            return;
        }
        if (UserInput.Instance.IsFireHeld)
        {
            if (!StopCharge)
            {
                ChargeBubble();
            }
        }
        else
        {
            ReleaseBubble();
        }
    }

    private void ChargeBubble()
    {
        if (Player.IsMagicEmpty)
        {
            StopCharge = true;
            Bubble.StopCharging();
            return;
        }
    }

    private void ReleaseBubble()
    {
        exit = true;
        Player.ChangeAttackState(new AttackPitchingState(Player, PlayerData, Bubble));
    }
}