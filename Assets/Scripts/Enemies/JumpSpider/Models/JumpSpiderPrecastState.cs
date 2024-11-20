using UnityEngine;

namespace Enemies.JumpSpider
{
    public class JumpSpiderPrecastState : IState
    {
        private JumpSpider JumpSpider;
        private JumpSpiderData Data;
        private Player Player;

        private float precastTimer;

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
                JumpSpider.SetState(new JumpSpiderJumpState(JumpSpider, Data, Player));
            }
        }
    }
}
