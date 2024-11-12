using UnityEngine;

public class PlayerAttackIdleState : IPlayerAttackState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }
    public bool ShouldShowAnimation { get; } = false;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="playerData">PlayerData</param>
    public PlayerAttackIdleState(Player player, PlayerData playerData)
    {
        Debug.Log("Inside Idle State");
        Player = player;
        PlayerData = playerData;
    }

    public void HandleAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Player.ChangePlayerAttackState(new PlayerAttackChargingState(Player, PlayerData, Player.InitialBubble()));
        }
    } 

    public void HandleKnockedBack() 
    {
        return;
    }
}
