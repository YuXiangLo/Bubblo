using UnityEngine;

public class PlayerAttackIdleState : IPlayerAttackState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }
    
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
        if (Input.GetButtonDown("Fire1"))
        {
            Player.ChangePlayerAttackState(new PlayerAttackChargingState(Player, PlayerData, Player.InitialBubble()));
			Player.Animator.SetBool("IsHoldingBubble", true);
        }
    } 
}
