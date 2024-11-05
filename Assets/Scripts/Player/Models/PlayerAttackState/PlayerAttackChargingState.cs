using UnityEngine;

public class PlayerAttackChargingState : IPlayerAttackState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }
    private Bubble CurrentBubble;
    private float HoldTime;
    private readonly bool IsExhaustedAtInit;
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
        Player = player;
        PlayerData = playerData;
        CurrentBubble = currentBubble;
        HoldTime = 0f;
        IsExhaustedAtInit = Player.CurrentMagicPoint <= 0f;
        MaxCharged = false;
    }

    public void HandleAttack()
    {
        if (CurrentBubble == null)
        {
            Player.ChangePlayerAttackState(new PlayerAttackIdleState(Player, PlayerData));
			Player.Animator.SetBool("IsAttack", false);
			Player.Animator.SetBool("IsHoldingBubble", false);
            return;
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
		Player.Animator.SetBool("IsAttack", true);
		Player.Animator.SetBool("IsHoldingBubble", false);
    }
}
