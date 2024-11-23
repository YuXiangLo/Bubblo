using UnityEngine;
namespace Enemies.JumpSpider
{
    public class JumpSpiderApproachState : IState
    {
        private JumpSpider JumpSpider;
        private JumpSpiderData Data;
        private Player Player;


        public JumpSpiderApproachState(JumpSpider jumpSpider, JumpSpiderData data, Player player)
        {
            JumpSpider = jumpSpider;
            Data = data;
            Player = player;
        }
        public void Enter()
        {
            JumpSpider.Animator.SetInteger("StateType", (int)JumpSpiderStateType.Approach);
        }

        public void Exit()
        {
            // Do nothing
        }

        public void Update()
        {
            HandleMovement();
            DetectDistance();
        }

        private void HandleMovement()
        {
            Vector2 playerPosition = Player.transform.position;
            float xDirection = (playerPosition.x - JumpSpider.transform.position.x) > 0 ? 1 : -1;
            
            if (xDirection == 1)
            {
                bool onBoarder = JumpSpider.transform.position.x > Data.RightPoint.x - 0.1f;
                JumpSpider.Velocity.x = onBoarder ? 0 : Data.Speed;
            }
            else
            {
                bool onBoarder = JumpSpider.transform.position.x < Data.LeftPoint.x + 0.1f;
                JumpSpider.Velocity.x = onBoarder ? 0 : -Data.Speed;
            }
        }
    
        public void DetectDistance()
        {
            Vector2 distanceToPlayer = Player.transform.position - JumpSpider.transform.position;
            if (Mathf.Abs(distanceToPlayer.x) < Data.ToJumpDistance && Mathf.Abs(distanceToPlayer.y) < Data.YDetectRange)
            {
                JumpSpider.SetState(new JumpSpiderPrecastState(JumpSpider, Data, Player));
            }
            else if (Mathf.Abs(distanceToPlayer.x) > Data.ToApproachDistance || Mathf.Abs(distanceToPlayer.y) > Data.YDetectRange)
            {
                JumpSpider.SetState(new JumpSpiderDefaultState(JumpSpider, Data, Player));
            }
        }
    }
}