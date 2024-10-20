using UnityEngine;

public class SimpleBeeMovement : MonoBehaviour
{
    private Transform PlayerTransform;
    private Transform LeftPoint, RightPoint;
    private Vector3 Target;
    private Vector3 NextPosition;
    private bool DefaultMovement = true;
    private bool FacingRight = true;
    [SerializeField] private float DetectionDistance = 5f;

    public float speed = 2f;
    private void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        LeftPoint = transform.parent.Find("LeftPoint");
        RightPoint = transform.parent.Find("RightPoint");
        Target = LeftPoint.position;
    }

    public void HandleMovement()
    {
        if (DetectPlayer())
            ApproachPlayer();
        if (DefaultMovement)
            DefaultMove();
        Move();
    }
    
    private void ApproachPlayer()
    {
        DefaultMovement = false;
        NextPosition = Vector2.MoveTowards(transform.position, PlayerTransform.position, speed * Time.deltaTime);
    }

    private void DefaultMove()
    {
        // Move enemy towards the target
        NextPosition = Vector2.MoveTowards(transform.position, Target, speed * Time.deltaTime);

        // Switch target when reaching the point
        if (Vector2.Distance(transform.position, Target) < 0.1f)
        {
            if (Target == LeftPoint.position)
                Target = RightPoint.position;
            else
                Target = LeftPoint.position;
        }
    }
    
    private void Move()
    {
        transform.position = NextPosition;
        Vector3 movementDirection = NextPosition - transform.position;
        if (movementDirection.x > 0 && !FacingRight)
        {
            Flip();
        }
        else if (movementDirection.x < 0 && FacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Toggle the facing direction
        FacingRight = !FacingRight;

        // Flip the enemy's sprite by scaling in the X axis
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;  // Flip the X scale
        transform.localScale = localScale;
    }

    private bool DetectPlayer()
    {
        float distance = Vector2.Distance(transform.position, PlayerTransform.position);
        return distance < DetectionDistance;
    }
}
