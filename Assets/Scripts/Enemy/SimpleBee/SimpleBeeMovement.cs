using UnityEngine;

namespace SimpleBee 
{
    public class SimpleBeeMovement : MonoBehaviour
    {
        [SerializeField] private Vector2 LeftPoint, RightPoint;
        [SerializeField] private float Speed = 2f;
        private Vector2 Target;
        private bool FacingLeft = true;

        private void Start()
        {
            Target = (Vector2)transform.position + LeftPoint;
        }

        public void HandleMovement()
        {
            Vector2 currentPosition = transform.position;
            // Move enemy towards the target
            transform.position = Vector2.MoveTowards(currentPosition, Target, Speed * Time.deltaTime);

            // Switch target when reaching the point
            if (Vector2.Distance(currentPosition, Target) < 0.1f)
            {
                GetNewTarget();
            }
        }

        private void GetNewTarget()
        {
            if (FacingLeft) {
                Flip();
                Target = (Vector2)transform.position + RightPoint;
            }
            else {
                Flip();
                Target = (Vector2)transform.position + LeftPoint;
            }
        }

        private void Flip()
        {
            // Toggle the facing direction
            FacingLeft = !FacingLeft;

            // Flip the enemy's sprite by scaling in the X axis
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;  // Flip the X scale
            transform.localScale = localScale;
        }
    }
}
