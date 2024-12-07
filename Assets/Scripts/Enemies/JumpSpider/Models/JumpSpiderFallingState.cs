using UnityEngine;

namespace Enemies.JumpSpider
{
    public class JumpSpiderFallingState : IState
    {
        private JumpSpider JumpSpider;
        private JumpSpiderData Data;
        private Player Player;
        private float OriginalYPosition;

        public JumpSpiderFallingState(JumpSpider jumpSpider, JumpSpiderData data, Player player, float originalYPosition)
        {
            JumpSpider = jumpSpider;
            Data = data;
            Player = player;
            OriginalYPosition = originalYPosition;
        }

        public void Enter()
        {
            JumpSpider.Animator.SetInteger("StateType", (int)JumpSpiderStateType.Falling);
            JumpSpider.Velocity.y = 0;
        }

        public void Exit()
        {
            // Do nothing
        }

        public void Update()
        {
            ApplyGravity();
            DetectGround();
        }

        private void ApplyGravity()
        {
            JumpSpider.Velocity.y -= Data.FallingMultiplier * Data.Gravity * Time.deltaTime;

        }

        private void DetectGround()
        {
            if (JumpSpider.transform.position.y < OriginalYPosition)
            {
                JumpSpider.Velocity.y = 0;
                JumpSpider.transform.position = new Vector2(JumpSpider.transform.position.x, OriginalYPosition);
                JumpSpider.SetState(new JumpSpiderDefaultState(JumpSpider, Data, Player));
            }
        }
    }
}