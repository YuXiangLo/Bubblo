using UnityEngine;

namespace Enemies.JumpSpider
{
    public class JumpSpiderDefaultState : IState
    {
        private JumpSpider JumpSpider;
        private JumpSpiderData Data;
        private Player Player;

        private Vector2 Target;

        public JumpSpiderDefaultState(JumpSpider jumpSpider, JumpSpiderData data, Player player)
        {
            JumpSpider = jumpSpider;
            Data = data;
            Player = player;
            Target = Data.LeftPoint;
        }

        public void Enter()
        {
            // Do nothing
        }

        public void Update()
        {
            HandleMovement();
            TargetDetection();
            DistanceDetection();
        }

        public void Exit()
        {
            // Do nothing
        }

        private void DistanceDetection()
        {
            Vector2 distanceToPlayer = Player.transform.position - JumpSpider.transform.position;
            if (Mathf.Abs(distanceToPlayer.x) < Data.ToApproachDistance && Mathf.Abs(distanceToPlayer.y) < Data.YDetectRange)
            {
                JumpSpider.SetState(new JumpSpiderApproachState(JumpSpider, Data, Player));
            }
        }

        private void TargetDetection()
        {
            if (JumpSpider.transform.position.x < Data.LeftPoint.x + 0.1f)
            {
                Target = Data.RightPoint;
            }
            else if (JumpSpider.transform.position.x > Data.RightPoint.x - 0.1f)
            {
                Target = Data.LeftPoint;
            }
        }

        private void HandleMovement()
        {
            Vector2 direction = (Target - (Vector2)JumpSpider.transform.position).normalized;
            JumpSpider.Velocity.x = direction.x * Data.Speed;
        }
    }
}