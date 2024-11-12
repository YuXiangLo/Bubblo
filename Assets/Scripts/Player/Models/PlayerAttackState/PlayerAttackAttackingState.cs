using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAttackingState : IPlayerAttackState
{
    public Player Player { get; }
    public PlayerData PlayerData { get; }
    public bool ShouldShowAnimation { get; } = true;

    public PlayerAttackAttackingState(Player player, PlayerData playerData)
    {
        Debug.Log("Inside Attacking State");
        Player = player;
        PlayerData = playerData;
        Player.Animator.SetInteger("PlayerState", (int)PlayerState.PlayerStateType.Attack);
    }

    public void HandleAttack()
    {
        var stateInfo = Player.Animator.GetCurrentAnimatorStateInfo(0);
        //!stateInfo.IsName("PlayerAttack")
        if (stateInfo.IsName("PlayerAttack") && stateInfo.normalizedTime >= 1.0f)
        {
            Debug.Log("Leave Attacking State");
            Player.ChangePlayerAttackState(new PlayerAttackIdleState(Player, PlayerData));
        }
    }

    public void HandleKnockedBack() 
    {
        Player.ChangePlayerAttackState(new PlayerAttackIdleState(Player, PlayerData));
    }
}
