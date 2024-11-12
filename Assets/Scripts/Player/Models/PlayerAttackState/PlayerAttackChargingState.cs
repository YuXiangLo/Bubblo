using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackChargingState : IPlayerAttackState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }
    public bool ShouldShowAnimation { get; } = true;

    private Bubble CurrentBubble;
    private readonly bool IsExhaustedAtInit;
    private float HoldTime;
    private bool MaxCharged;
    private const float MaxHoldTime = 2f;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="playerData">PlayerData</param>
    /// <param name="currentBubble">Bubble Player's Holding</param>
    public PlayerAttackChargingState(Player player, PlayerData playerData, Bubble currentBubble)
    {
        Debug.Log("Inside Charging State");
        Player = player;
        PlayerData = playerData;
        CurrentBubble = currentBubble;
        HoldTime = 0f;
        IsExhaustedAtInit = Player.CurrentMagicPoint <= 0f;
        MaxCharged = false;
        Player.Animator.SetInteger("PlayerState", (int)PlayerState.PlayerStateType.GenerateBubble);
    }

    public void HandleAttack()
    {
        if (CurrentBubble == null)
        {
            Player.ChangePlayerAttackState(new PlayerAttackAttackingState(Player, PlayerData));
        }

        if (IsExhaustedAtInit || Input.GetButtonUp("Fire1"))
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

    public void HandleKnockedBack() 
    {
        if (CurrentBubble != null)
        {
            CurrentBubble.Remove();
        }

        Player.ChangePlayerAttackState(new PlayerAttackIdleState(Player, PlayerData));
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
    }
}
