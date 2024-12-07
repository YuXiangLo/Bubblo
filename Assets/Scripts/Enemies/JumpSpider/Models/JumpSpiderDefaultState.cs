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
            JumpSpider.Animator.SetInteger("StateType", (int)JumpSpiderStateType.Default);
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
            float distanceX = Mathf.Abs(Player.transform.position.x - JumpSpider.transform.position.x);
            float distanceY = Mathf.Abs(Player.transform.position.y - JumpSpider.transform.position.y);
            bool inApproachRange = distanceX < Data.ToApproachDistance && distanceY < Data.ToApproachDistance;
            bool PlayerInMovementRange = Player.transform.position.x > Data.LeftPoint.x && Player.transform.position.x < Data.RightPoint.x;
            if (inApproachRange && PlayerInMovementRange)
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