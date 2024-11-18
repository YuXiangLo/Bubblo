using UnityEngine;

namespace Enemies.AssaultBee
{
    public class AssaultBee : MonoBehaviour, IEnemyModifyHealth
    {
        private EnemyHealth Health;
        private AssaultBeeData Data;
        private IState CurrentState;
        private Player Player;

        public Vector2 Velocity = Vector2.zero;

        private void Awake()
        {
            Health = GetComponent<EnemyHealth>();
            Data = GetComponent<AssaultBeeData>();
            Player = GameObject.FindWithTag("Player").GetComponent<Player>();
            Data.LeftPoint = (Vector2)transform.position + Data.LeftPoint;
            Data.RightPoint = (Vector2)transform.position + Data.RightPoint;
            CurrentState = new DefaultState(this, Data, Player);
        }

        public void SetState(IState state)
        {
            CurrentState.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }

        private void Update()
        {
            CurrentState.Update();
            HandleFaceDirection();
        }

        private void FixedUpdate()
        {
            transform.position = (Vector2)transform.position + Velocity * Time.deltaTime;
        }

        public void Heal(float amount)
        {
            Health.Heal(amount);
        }

        public void TakeDamage(float amount)
        {
            Health.TakeDamage(amount);
        }

        private void HandleFaceDirection()
        {
            bool isMovingLeft = Velocity.x < 0;
            Vector3 localScale = transform.localScale;
            float absScaleX = Mathf.Abs(localScale.x);
            if (Data.IsFacingLeft != (Velocity.x < 0))
            {
                Data.IsFacingLeft = isMovingLeft;
                transform.localScale = new Vector3(isMovingLeft ? absScaleX : -absScaleX, localScale.y, localScale.z);
            }
        }
    }
}