using UnityEngine;

public class SimpleBeeMovement : MonoBehaviour
{
    private Player Player;
    private Transform LeftPoint, RightPoint;
    private Vector3 DefaultTarget;
    private Vector3 NextPosition;
    private bool FacingLeft = true;
    private float CurrentAttackCD = 0f;
    private bool IsAttacking = false;
    private bool IsApproaching = true;
    private Vector3 Target;
    private Vector3 RestoredPosition;
    [SerializeField] private float AttackCD = 3f;
    [SerializeField] private float DetectionDistance = 7f;

    [SerializeField] private float Speed = 2f;
    [SerializeField] private float ApproachSpeed = 10f;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        LeftPoint = transform.parent.Find("LeftPoint");
        RightPoint = transform.parent.Find("RightPoint");
        DefaultTarget = LeftPoint.position;
    }

    public void HandleMovement()
    {
        if (DetectPlayer() && CurrentAttackCD <= 0f)
        {
            IsApproaching = true;
            IsAttacking = true;
            Target = Player.transform.position;
            RestoredPosition = transform.position;
            CurrentAttackCD = AttackCD;
        }
        if (CurrentAttackCD > 0f)
        {
            CurrentAttackCD -= Time.deltaTime;
        }

        if (IsAttacking)
        {
            AttackMovement();
        }
        else
        {
            DefaultMovement();
        }
    }
    
    private void AttackMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target, ApproachSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, Target) < 0.1f)
        {
            Target = RestoredPosition;
            IsApproaching = false;
        }
        if (!IsApproaching && Vector2.Distance(transform.position, RestoredPosition) < 0.1f)
        {
            IsAttacking = false;
            transform.position = RestoredPosition;
        }
    }

    private void DefaultMovement()
    {
        // Move enemy towards the target
        NextPosition = Vector2.MoveTowards(transform.position, DefaultTarget, Speed * Time.deltaTime);

        // Switch target when reaching the point
        if (Vector2.Distance(transform.position, DefaultTarget) < 0.1f)
        {
            if (DefaultTarget == LeftPoint.position)
                DefaultTarget = RightPoint.position;
            else
                DefaultTarget = LeftPoint.position;
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
        // Toggle the facing direction
        FacingLeft = !FacingLeft;

        // Flip the enemy's sprite by scaling in the X axis
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;  // Flip the X scale
        transform.localScale = localScale;
    }

    private bool DetectPlayer()
    {
        return Vector2.Distance(transform.position, Player.transform.position) < DetectionDistance;
    }
}
