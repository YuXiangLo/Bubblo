using UnityEngine;

public class MovementEnterLevelState : IMovementState
{
    private readonly Player Player;
    private readonly PlayerData PlayerData;

    public bool AttackEnabled => false;
    public MovementEnterLevelState(Player player, PlayerData playerData)
    {
        Player = player;
        PlayerData = playerData;
    }

    public void Enter()
    {
        
    }

    public void Update()
    {
    }
}