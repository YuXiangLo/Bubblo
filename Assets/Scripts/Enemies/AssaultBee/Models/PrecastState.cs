using UnityEngine;

namespace Enemies.AssaultBee
{
    public class PrecastState : IState
    {
        private AssaultBee AssaultBee;
        private AssaultBeeData Data;
        private Player Player;

        private float PrecastTimer;
        private Vector2 Source;
        private Vector2 Direction;

        public PrecastState(AssaultBee assaultBee, AssaultBeeData data, Player player)
        {
            AssaultBee = assaultBee;
            Data = data;
            Player = player;
        }

        public void Enter()
        {
            AssaultBee.IsBackward = true;
            PrecastTimer = Data.PrecastAnimation.length;
            AssaultBee.Animator.SetInteger("State", (int)StateType.Precast);
            Source = AssaultBee.transform.position;
            Direction = -((Vector2)Player.transform.position - Source).normalized;
            AssaultBee.Velocity = Direction * Data.PrecastSpeedMultiplier * Data.Speed;
        }

        public void Exit()
        {
            AssaultBee.IsBackward = false;
        }

        public void Update()
        {
            PrecastTimer -= Time.deltaTime;

            if (PrecastTimer <= 0)
            {
                AssaultBee.SetState(new ApproachState(AssaultBee, Data, Player, Source));
            }
        }
    }
}