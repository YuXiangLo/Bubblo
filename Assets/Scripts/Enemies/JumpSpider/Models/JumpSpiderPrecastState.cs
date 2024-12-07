using UnityEngine;

namespace Enemies.JumpSpider
{
    public class JumpSpiderPrecastState : IState
    {
        private JumpSpider JumpSpider;
        private JumpSpiderData Data;
        private Player Player;

        private Vector2 JumpVelocity = Vector2.zero;
        private float PrecastTimer;

        public JumpSpiderPrecastState(JumpSpider jumpSpider, JumpSpiderData data, Player player)
        {
            JumpSpider = jumpSpider;
            Data = data;
            Player = player;
        }

        public void Enter()
        {
            JumpSpider.Animator.Play("Precast");
            JumpSpider.Velocity = Vector2.zero;
            JumpVelocity = GetJumpVelocity();
            DirectionInitialize();
            PrecastTimer = Data.PrecastAnimation.length;
            JumpSpider.Animator.SetInteger("StateType", (int)JumpSpiderStateType.Precast);
        }

        public void Exit()
        {
            // No additional cleanup required
        }

        public void Update()
        {
            PrecastTimer -= Time.deltaTime;

            if (PrecastTimer <= 0)
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

        private void DirectionInitialize()
        {
            var scale = JumpSpider.transform.localScale;
            if (JumpVelocity.x <= 0)
            {
                scale.x = Mathf.Abs(scale.x);
            }
            else
            {
                scale.x = -Mathf.Abs(scale.x);
            }
            JumpSpider.transform.localScale = scale;
        }
    }
}
