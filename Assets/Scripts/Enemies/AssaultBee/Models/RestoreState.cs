using UnityEngine;

namespace Enemies.AssaultBee
{
    public class RestoreState : IState
    {
        private AssaultBee AssaultBee;
        private AssaultBeeData Data;
        private Player Player;
        private Vector2 Target;

        public RestoreState(AssaultBee assaultBee, AssaultBeeData data, Player player, Vector2 target)
        {
            AssaultBee = assaultBee;
            Data = data;
            Player = player;
            Target = target;
        }

        public void Enter()
        {
            AssaultBee.Velocity = (Target - (Vector2)AssaultBee.transform.position).normalized * Data.Speed;
            AssaultBee.Animator.SetInteger("State", (int)StateType.Restore);
            AssaultBee.IsBackward = true;
        }

        public void Exit()
        {
            AssaultBee.Velocity = Vector2.zero;
            AssaultBee.IsBackward = false;
        }

        public void Update()
        {
            DetectReturn();
        }

        private void DetectReturn()
        {
            if (Vector2.Distance((Vector2)AssaultBee.transform.position, Target) < 0.1f)
            {
                AssaultBee.SetState(new DefaultState(AssaultBee, Data, Player));
            }
        }
    }
}
