using System.Runtime.InteropServices;
using UnityEngine;

namespace Enemies.AssaultBee
{
    public class DefaultState : IState
    {
        private AssaultBee AssaultBee;
        private AssaultBeeData Data;
        private Player Player;

        private Vector2 Target;

        public DefaultState(AssaultBee assaultBee, AssaultBeeData data, Player player)
        {
            AssaultBee = assaultBee;
            Data = data;
            Player = player;
            Target = Data.LeftPoint;
        }

        public void Enter()
        {
            AssaultBee.Animator.SetInteger("State", (int)StateType.Default);
        }

        public void Exit()
        {
            // Do nothing
        }

        public void Update()
        {
            HandleMovement();
            TargetDetection();
            DetectDistance();
        }

        private void HandleMovement()
        {
            Vector2 direction = (Target - (Vector2)AssaultBee.transform.position).normalized;
            AssaultBee.Velocity.x = direction.x * Data.Speed;
        }

        private void DetectDistance()
        {
            Vector2 distanceToPlayer = Player.transform.position - AssaultBee.transform.position;
            if (distanceToPlayer.magnitude < Data.DetectionDistance)
            {  
                AssaultBee.SetState(new PrecastState(AssaultBee, Data, Player));
            }
        }

        private void TargetDetection()
        {
            if (AssaultBee.transform.position.x < Data.LeftPoint.x + 0.1f)
            {
                Target = Data.RightPoint;
            }
            else if (AssaultBee.transform.position.x > Data.RightPoint.x - 0.1f)
            {
                Target = Data.LeftPoint;
            }
        }
    }
}