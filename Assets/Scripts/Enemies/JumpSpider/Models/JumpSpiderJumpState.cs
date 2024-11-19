using UnityEngine;

namespace Enemies.JumpSpider
{
    public class JumpSpiderJumpState : IState
    {
        private JumpSpider JumpSpider;
        private JumpSpiderData Data;
        private Player Player;
        private float OriginalYPosition;

        public JumpSpiderJumpState(JumpSpider jumpSpider, JumpSpiderData data, Player player)
        {
            JumpSpider = jumpSpider;
            Data = data;
            Player = player;
        }

        public void Enter()
        {
            JumpSpider.Animator.SetInteger("StateType", (int)JumpSpiderStateType.Jump);
            OriginalYPosition = JumpSpider.transform.position.y;
            Jump();
        }

        public void Exit()
        {
            // Do nothing
        }

        public void Update()
        {
            ApplyGravity();
            DetectFalling();
        }

        private void Jump()
        {
            JumpSpider.Velocity.y = Data.JumpForce;
            float xDistanceToPlayer = Player.transform.position.x - JumpSpider.transform.position.x;
            
            // Decide if the boarder or the player is jump target
            float xTarget = Player.transform.position.x;
            if (JumpSpider.transform.position.x < Data.LeftPoint.x + 0.1f && xDistanceToPlayer < 0)
            {
                xTarget = Data.LeftPoint.x;
            }
            else if (JumpSpider.transform.position.x > Data.RightPoint.x - 0.1f && xDistanceToPlayer > 0)
            {
                xTarget = Data.RightPoint.x;
            }
            
            float xDistance = xTarget - JumpSpider.transform.position.x;
            float timeToPeakAndFall = CalculateTimeToPeakAndFall();
            JumpSpider.Velocity.x = xDistance / timeToPeakAndFall;
        }

        private float CalculateTimeToPeakAndFall()
        {
            float timeToPeak = Data.JumpForce / Data.Gravity;
            return 2 * timeToPeak;
        }

        private void ApplyGravity()
        {
            JumpSpider.Velocity.y -= Data.Gravity * Time.deltaTime;
        }
        
        private void DetectFalling()
        {
            if (JumpSpider.Velocity.y < 0)
            {
                JumpSpider.SetState(new JumpSpiderFallingState(JumpSpider, Data, Player, OriginalYPosition));
            }
        }
    }
}