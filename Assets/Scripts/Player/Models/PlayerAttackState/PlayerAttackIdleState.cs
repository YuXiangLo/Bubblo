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
        Player = player;
        PlayerData = playerData;
    }

    public void HandleAttack()
    {
        if (UserInput.Instance.Fire)
        {
            Player.ChangePlayerAttackState(new PlayerAttackChargingState(Player, PlayerData, Player.InitialBubble()));
        }
    } 

    public void HandleKnockedBack() 
    {
        return;
    }
}
