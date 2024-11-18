using UnityEngine;

namespace Enemies.JumpSpider
{
    public class JumpSpiderPrecastState : IState
    {
        private JumpSpider JumpSpider;
        private JumpSpiderData Data;
        private Player Player;

        public JumpSpiderPrecastState(JumpSpider jumpSpider, JumpSpiderData data, Player player)
        {
            JumpSpider = jumpSpider;
            Data = data;
            Player = player;
        }

        public void Enter()
        {
            JumpSpider.Animator.SetInteger("StateType", (int)JumpSpiderStateType.Precast);
            JumpSpider.Velocity = Vector2.zero;
        }

        public void Exit()
        {
            // Do nothing
        }

        public void Update()
        {
            var stateInfo = JumpSpider.Animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Precast") && stateInfo.normalizedTime >= 1f)
            {
                JumpSpider.SetState(new JumpSpiderJumpState(JumpSpider, Data, Player));
            }
        }
    }
}