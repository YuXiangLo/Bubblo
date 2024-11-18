using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAttackingState : IPlayerAttackState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }
    public bool ShouldShowAnimation { get; } = true;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="player">Player</param>
    /// <param name="playerData">PlayerData</param>
    public PlayerAttackAttackingState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
        Player.SetAnimation(PlayerStateType.Attack);
    }

    public void HandleAttack()
    {
        var stateInfo = Player.Animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("PlayerAttack") && stateInfo.normalizedTime >= 0.5f)
        {
            Player.ChangePlayerAttackState(new PlayerAttackIdleState(Player, PlayerData));
        }
    }

    public void HandleKnockedBack() 
    {
        Player.ChangePlayerAttackState(new PlayerAttackIdleState(Player, PlayerData));
    }
}
