using UnityEngine;

namespace Enemies.AssaultBee
{
    public class PrecastSate : IState
    {
        private AssaultBee AssaultBee;
        private AssaultBeeData Data;
        private Player Player;

        private float precastTimer = 0f;
        private Vector2 Source;
        private Vector2 Direction;

        public PrecastSate(AssaultBee assaultBee, AssaultBeeData data, Player player)
        {
            AssaultBee = assaultBee;
            Data = data;
            Player = player;
        }

        public void Enter()
        {
            precastTimer = 0f;
            AssaultBee.Animator.SetInteger("State", (int)StateType.Precast);
            Source = AssaultBee.transform.position;
            Direction = -((Vector2)Player.transform.position - Source).normalized;
            AssaultBee.Velocity = Direction * Data.PrecastSpeedMultiplier * Data.Speed;

            AssaultBee.IsBackward = true;
        }

        public void Exit()
        {
            AssaultBee.IsBackward = false;
        }

        public void Update()
        {
            precastTimer += Time.deltaTime;

            var stateInfo = AssaultBee.Animator.GetCurrentAnimatorStateInfo(0);
            if (precastTimer >= stateInfo.length)
            {
                AssaultBee.SetState(new ApproachState(AssaultBee, Data, Player, Source));
            }
        }
    }
}