using UnityEngine;

namespace Enemies.JumpSpider
{
    public class JumpSpiderPrecastState : IState
    {
        private JumpSpider JumpSpider;
        private JumpSpiderData Data;
        private Player Player;

        private float precastTimer;
        private Vector2 JumpVelocity = Vector2.zero;

        public JumpSpiderPrecastState(JumpSpider jumpSpider, JumpSpiderData data, Player player)
        {
            JumpSpider = jumpSpider;
            Data = data;
            Player = player;
        }

        public void Enter()
        {
            precastTimer = 0f;
            JumpSpider.Animator.Play("Precast");
            JumpSpider.Velocity = Vector2.zero;
            JumpVelocity = GetJumpVelocity();
        }

        public void Exit()
        {
            // No additional cleanup required
        }

        public void Update()
        {
            precastTimer += Time.deltaTime;

            var stateInfo = JumpSpider.Animator.GetCurrentAnimatorStateInfo(0);
            if (precastTimer >= stateInfo.length)
            {
                JumpSpider.SetState(new JumpSpiderJumpState(JumpSpider, Data, Player, JumpVelocity));
            }
        }

        private Vector2 GetJumpVelocity()
        {
            Vector2 result = Vector2.zero;
            result.y = Data.JumpForce;
            float xDistance = Player.transform.position.x - JumpSpider.transform.position.x;
            float timeToPeakAndFall = CalculateTimeToPeakAndFall();
            result.x = xDistance / timeToPeakAndFall;
            return result;
        }

        private float CalculateTimeToPeakAndFall()
        {
            float timeToPeak = Data.JumpForce / Data.Gravity;
            return 2 * timeToPeak;
        }
    }
}
