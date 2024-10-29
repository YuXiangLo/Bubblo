using UnityEngine;

public class PlayerAttackIdleState : IPlayerAttackState
{
    public PlayerControl PlayerControl { get; }
    public PlayerData PlayerData { get; }
    
    public PlayerAttackIdleState(PlayerControl playerControl, PlayerData playerData)
    {
        PlayerControl = playerControl;
        PlayerData = playerData;
    }

    public void HandleAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PlayerControl.ChangePlayerAttackState(new PlayerAttackChargingState(PlayerControl, PlayerData, PlayerControl.InitialBubble()));
            PlayerControl.IsHoldingBubble = true;
        }
    } 
}
