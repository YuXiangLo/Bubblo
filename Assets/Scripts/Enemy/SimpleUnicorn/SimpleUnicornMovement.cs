using UnityEngine;

public class SimpleUnicornMovement : MonoBehaviour
{
    private Transform LeftPoint, RightPoint;
    private Vector3 Target;
    private bool FacingLeft = true;

    public float Speed = 2f;
    private void Start()
    {
        LeftPoint = transform.parent.Find("LeftPoint");
        RightPoint = transform.parent.Find("RightPoint");
        Target = LeftPoint.position;
    }

    public void HandleMovement()
    {
        Move();
        RotateToMovementDirection();
    }

    private void Move()
    {
        // Move enemy towards the target
        transform.position = Vector2.MoveTowards(transform.position, Target, Speed * Time.deltaTime);

        // Switch target when reaching the point
        if (Vector2.Distance(transform.position, Target) < 0.1f)
        {
            if (Target == LeftPoint.position)
                Target = RightPoint.position;
            else
                Target = LeftPoint.position;
        }
    }

    private void RotateToMovementDirection()
    {
        // Calculate the movement direction
        Vector3 movementDirection = Target - transform.position;

        // Check if moving to the right
        if (movementDirection.x > 0 && !FacingLeft)
        {
            Flip();
        }
        // Check if moving to the left
        else if (movementDirection.x < 0 && FacingLeft)
        {
            Flip();
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
