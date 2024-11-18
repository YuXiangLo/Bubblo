using UnityEngine;

namespace Enemies.JumpSpider
{
    public class JumpSpider : MonoBehaviour, IEnemyModifyHealth
    {
        private EnemyHealth SimpleSpiderHealth;
        private JumpSpiderData Data;
        private IJumpSpiderState CurrentState;
        private Player Player;

        public Vector2 Velocity = Vector2.zero;

        private void Awake()
        {
            SimpleSpiderHealth = GetComponent<EnemyHealth>();
            Data = GetComponent<JumpSpiderData>();
            Player = GameObject.FindWithTag("Player").GetComponent<Player>();
            Data.LeftPoint = (Vector2)transform.position + Data.LeftPoint;
            Data.RightPoint = (Vector2)transform.position + Data.RightPoint;
            CurrentState = new JumpSpiderDefaultState(this, Data, Player);
        }

        public void ChangeJumpSpiderState(IJumpSpiderState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
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
            SimpleSpiderHealth.Heal(amount);
        }

        public void TakeDamage(float amount)
        {
            SimpleSpiderHealth.TakeDamage(amount);
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