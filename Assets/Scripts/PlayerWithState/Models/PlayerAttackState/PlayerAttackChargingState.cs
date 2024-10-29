using UnityEngine;

public class PlayerAttackChargingState : IPlayerAttackState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }
    private Bubble CurrentBubble;
    private float HoldTime;
    private bool MaxCharged;
    private const float MaxHoldTime = 2f;
    
    public PlayerAttackChargingState(Player player, PlayerData playerData, Bubble currentBubble)
    {
        Player = player;
        PlayerData = playerData;
        CurrentBubble = currentBubble;
        HoldTime = 0f;
        MaxCharged = false;
    }

    public void HandleAttack()
    {
        if (CurrentBubble == null)
        {
            Player.ChangePlayerAttackState(new PlayerAttackIdleState(Player, PlayerData));
            Player.IsAttacking = false;
            Player.IsHoldingBubble = false;
            return;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            ReleaseBubble();
        }
        else if (Input.GetButton("Fire1"))
        {
            if (!MaxCharged)
            {
                ChargeBubble();
            }
        }
    }

    private void ChargeBubble()
    {
        if (Player.CurrentMagicPoint > 0 && HoldTime < MaxHoldTime) 
        {
            Player.CurrentMagicPoint -= Time.deltaTime;
            HoldTime += Time.deltaTime;
        }
        else
        {
            MaxCharged = true;
            StopChargingBubble();
        }
    }

    private void StopChargingBubble() 
    {
        CurrentBubble.StopCharging();
    }
    private void ReleaseBubble() 
    {
        CurrentBubble.Release();
        CurrentBubble = null;
        Player.IsAttacking = true;
    }
}
