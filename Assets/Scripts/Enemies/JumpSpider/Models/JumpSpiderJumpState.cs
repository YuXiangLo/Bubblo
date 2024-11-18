using UnityEngine;

namespace Enemies.JumpSpider
{
    public class JumpSpiderJumpState : IJumpSpiderState
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
            JumpSpider.Velocity.y = CalculateVerticalVelocity();
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
            Debug.Log($"Jump => OriginalY: {OriginalYPosition}, Velocity.x: {JumpSpider.Velocity.x}, Velocity.y: {JumpSpider.Velocity.y}");
        }

        private float CalculateVerticalVelocity()
        {
            // v = sqrt(2 * g * h)
            return Mathf.Sqrt(2 * Data.Gravity * Data.JumpHeight);
        }

        private float CalculateTimeToPeakAndFall()
        {
            float timeToPeak = CalculateVerticalVelocity() / Data.Gravity;
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
                JumpSpider.ChangeJumpSpiderState(new JumpSpiderFallingState(JumpSpider, Data, Player, OriginalYPosition));
            }
        }
    }
}