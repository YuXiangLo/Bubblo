using UnityEngine;

public class SimpleSpiderMovement : MonoBehaviour
{
    private Transform PlayerTransform;
    private Transform LeftPoint, RightPoint;
    private Vector3 Target;
    private Vector3 NextPosition;
    private bool FacingLeft = true;
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
        else
            DefaultMove();
        Move();
    }
    
    private void ApproachPlayer()
    {
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
        if (movementDirection.x > 0 && !FacingLeft)
        {
            Flip();
        }
        else if (movementDirection.x < 0 && FacingLeft)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingLeft = !FacingLeft;
        transform.Rotate(0f, 180f, 0f);
    }

    private bool DetectPlayer()
    {
        return Vector2.Distance(transform.position, PlayerTransform.position) < DetectionDistance;
    }
}