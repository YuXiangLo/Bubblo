using UnityEngine;

public class SimpleUnicornMovement : MonoBehaviour
{
    private Transform LeftPoint, RightPoint;
    private Vector3 target;
    private bool facingRight = true;

    public float speed = 2f;
    private void Start()
    {
        LeftPoint = transform.parent.Find("LeftPoint");
        RightPoint = transform.parent.Find("RightPoint");
        target = LeftPoint.position;
    }

    public void HandleMovement()
    {
        Move();
        RotateToMovementDirection();
    }

    private void Move()
    {
        // Move enemy towards the target
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Switch target when reaching the point
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            if (target == LeftPoint.position)
                target = RightPoint.position;
            else
                target = LeftPoint.position;
        }
    }

    private void RotateToMovementDirection()
    {
        // Calculate the movement direction
        Vector3 movementDirection = target - transform.position;

        // Check if moving to the right
        if (movementDirection.x > 0 && !facingRight)
        {
            Flip();
        }
        // Check if moving to the left
        else if (movementDirection.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Toggle the facing direction
        facingRight = !facingRight;

        // Flip the enemy's sprite by scaling in the X axis
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;  // Flip the X scale
        transform.localScale = localScale;
    }
}
