using UnityEngine;

namespace Enemies.JumpSpider
{
    public class JumpSpiderJumpState : IState
    {
        private JumpSpider JumpSpider;
        private JumpSpiderData Data;
        private Player Player;
        private float OriginalYPosition;
        private Vector2 JumpVelocity;

        public JumpSpiderJumpState(JumpSpider jumpSpider, JumpSpiderData data, Player player, Vector2 jumpVelocity)
        {
            JumpSpider = jumpSpider;
            Data = data;
            Player = player;
            JumpVelocity = jumpVelocity;
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
            JumpSpider.Velocity = JumpVelocity;
            DirectionInitialize();
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