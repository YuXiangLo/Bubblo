using UnityEngine;
public class AttackChargingState : IAttackState
{
    private Player Player;
    private PlayerData PlayerData;
    private Bubble Bubble;
    public bool LockAnimation => true;

    private float HoldTime = 0f;
    private bool StopCharge = false;

    public AttackChargingState(Player player, PlayerData playerData, Bubble bubble)
    {
        Player = player;
        PlayerData = playerData;
        Bubble = bubble;
    }

    public void Enter()
    {
        // Do nothing
        Player.SetAnimation(AnimationStateType.Charging);
        Update();
    }

    public void Knockbacked()
    {
        Bubble.Remove();
        Player.ChangeAttackState(new AttackIdleState(Player, PlayerData));
    }

    public void Update()
    {
        if (UserInput.Instance.IsFireHeld)
        {
            if (!StopCharge)
            {
                ChargeBubble();
            }
        }
        else
        {
            ReleaseBubble();
        }
    }

    private void ChargeBubble()
    {
        if (Player.IsMagicEmpty || HoldTime >= 3 * PlayerData.CHARGE_INTERVAL)
        {
            StopCharge = true;
            Bubble.StopCharging();
            return;
        }

        HoldTime += Time.deltaTime;
        Player.Consume(Time.deltaTime);
    }

    private void ReleaseBubble()
    {
        Player.ChangeAttackState(new AttackPitchingState(Player, PlayerData, Bubble));
    }
}