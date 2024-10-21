using UnityEngine;

public class BossUnicornMovement : MonoBehaviour
{
    private Transform LeftPoint, RightPoint;
    private Vector3 Target;
    private bool FacingLeft = true;

    private Player Player;


    [SerializeField] private float DetectionRange = 5f;
    [SerializeField] private float Speed = 2f;
    [SerializeField] private float SprintSpeed = 4f;
    

    private void Start()
    {
        LeftPoint = transform.parent.Find("LeftPoint");
        RightPoint = transform.parent.Find("RightPoint");
        Target = LeftPoint.position;
        Player = FindObjectOfType<Player>();
    }

    public void HandleMovement()
    {
        Debug.Log("DetectPlayer: " + DetectPlayer());
        if (DetectPlayer())
        {
            AttackMovement();
        }
        else
        {
            DefaultMovement();
        }
    }

    private void DefaultMovement()
    {
        Vector3 movementDirection = Target - transform.position;
        RotateToMovementDirection(movementDirection);
        transform.position = Vector2.MoveTowards(transform.position, Target, Speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, Target) < 0.1f)
        {
            if (Target == LeftPoint.position)
                Target = RightPoint.position;
            else
                Target = LeftPoint.position;
        }
    }

    private void AttackMovement()
    {
        Vector3 movementDirection = Player.transform.position - transform.position;
        RotateToMovementDirection(movementDirection);
        transform.position = Vector2.MoveTowards(transform.position, Target, SprintSpeed * Time.deltaTime);
    }

    private bool DetectPlayer()
    {
        return Vector2.Distance(transform.position, Player.transform.position) < DetectionRange;
    }

    private void RotateToMovementDirection(Vector3 movementDirection)
    {
        if (movementDirection.x > 0 && FacingLeft)
        {
            Flip();
        }
        else if (movementDirection.x < 0 && !FacingLeft)
        {
            Flip();
        }
    }

    private void Flip()
    {
        FacingLeft = !FacingLeft;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}