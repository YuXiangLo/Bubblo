using UnityEngine;

public class PlayerAttackChargingState : IPlayerAttackState
{
    public PlayerControl PlayerControl { get; }
    public PlayerData PlayerData { get; }
    private Bubble CurrentBubble;
    private float HoldTime;
    private bool MaxCharged;
    private const float MaxHoldTime = 2f;
    
    public PlayerAttackChargingState(PlayerControl playerControl, PlayerData playerData, Bubble currentBubble)
    {
        PlayerControl = playerControl;
        PlayerData = playerData;
        CurrentBubble = currentBubble;
        HoldTime = 0f;
        MaxCharged = false;
    }

    public void HandleAttack()
    {
        if (CurrentBubble == null)
        {
            PlayerControl.ChangePlayerAttackState(new PlayerAttackIdleState(PlayerControl, PlayerData));
            PlayerControl.IsAttacking = false;
            PlayerControl.IsHoldingBubble = false;
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
        if (PlayerControl.CurrentMagicPoint > 0 && HoldTime < MaxHoldTime) 
        {
            PlayerControl.CurrentMagicPoint -= Time.deltaTime;
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
        PlayerControl.IsAttacking = true;
    }
}
