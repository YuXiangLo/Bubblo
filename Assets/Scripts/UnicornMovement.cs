using UnityEngine;

public class UnicornMovement : MonoBehaviour
{
    private Transform pointA, pointB;
    private Vector3 target;
    private bool facingRight = true;

    public float speed = 2f;
    void Start()
    {
        pointA = transform.parent.Find("PointA");
        pointB = transform.parent.Find("PointB");
        target = pointB.position;
    }

    void Update()
    {
        Move();
        RotateToMovementDirection();
    }

    void Move()
    {
        // Move enemy towards the target
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Switch target when reaching the point
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            if (target == pointA.position)
                target = pointB.position;
            else
                target = pointA.position;
        }
    }

    void RotateToMovementDirection()
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

    void Flip()
    {
        // Toggle the facing direction
        facingRight = !facingRight;

        // Flip the enemy's sprite by scaling in the X axis
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;  // Flip the X scale
        transform.localScale = localScale;
    }
}
