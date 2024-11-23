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
        private Vector2 InitialDirection;

        public ApproachState(AssaultBee assaultBee, AssaultBeeData data, Player player)
        {
            AssaultBee = assaultBee;
            Data = data;
            Player = player;
            Target = Player.transform.position;
            AssaultBee.Velocity = (Target - (Vector2)AssaultBee.transform.position).normalized * Data.AttackSpeedMultiplier * Data.Speed;
            Source = AssaultBee.transform.position;
            InitialDirection = (Target - Source).normalized;
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
            DetectReachOrPass();
        }

        private void DetectReachOrPass()
        {
            Vector2 currentPosition = (Vector2)AssaultBee.transform.position;
            Vector2 currentDirection = (Target - currentPosition).normalized;

            // Check if the AssaultBee has passed the target
            if (Vector2.Dot(InitialDirection, currentDirection) < 0 || Vector2.Distance(currentPosition, Target) < 0.1f)
            {
                AssaultBee.SetState(new RestoreState(AssaultBee, Data, Player, Source));
            }
        }
    }
}
