using UnityEngine;

namespace Enemies.AssaultBee
{
    public class ApproachState : IState
    {
        private AssaultBee AssaultBee;
        private AssaultBeeData Data;
        private Player Player;
        private Vector2 Target;
        private Vector2 Source;

        public ApproachState(AssaultBee assaultBee, AssaultBeeData data, Player player)
        {
            AssaultBee = assaultBee;
            Data = data;
            Player = player;
            Target = Player.transform.position;
            AssaultBee.Velocity = (Target - (Vector2)AssaultBee.transform.position).normalized * Data.AttackSpeedMultiplier * Data.Speed;
            Source = AssaultBee.transform.position;
        }

        public void Enter()
        {
            // Do nothing
        }

        public void Exit()
        {
            AssaultBee.Velocity = Vector2.zero;
        }

        public void Update()
        {
            DetectReach();
        }

        private void DetectReach()
        {
            if (Vector2.Distance((Vector2)AssaultBee.transform.position, Target) < 0.1f)
            {
                AssaultBee.SetState(new RestoreState(AssaultBee, Data, Player, Source));
            }
        }
    }
}