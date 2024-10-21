using UnityEngine;

public class BossUnicornMovement : MonoBehaviour
{
    private Transform LeftPoint, RightPoint;
    private Vector3 Target;
    private bool FacingLeft = true;

    private Player Player;

    [SerializeField] private GameObject ThornMissilePrefab;
    [SerializeField] private float SprintRange = 7f;
    [SerializeField] private float AttackRange = 4f;
    [SerializeField] private float Speed = 2f;
    [SerializeField] private float SprintSpeed = 4f;
    [SerializeField] private float AttackCD = 2f;
    [SerializeField] private float CurrentAttackCD = 0f;
    

    private void Start()
    {
        LeftPoint = transform.parent.Find("LeftPoint");
        RightPoint = transform.parent.Find("RightPoint");
        Target = LeftPoint.position;
        Player = FindObjectOfType<Player>();
    }

    public void HandleMovement()
    {
        if (DetectPlayer(AttackRange))
        {
            AttackMovement();
        }
        else if (DetectPlayer(SprintRange))
        {
            SprintMovement();
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

    private void SprintMovement()
    {
        Vector3 movementDirection = new(Player.transform.position.x - transform.position.x, 0, 0);
        RotateToMovementDirection(movementDirection);
        transform.position = Vector2.MoveTowards(transform.position, transform.position + movementDirection, SprintSpeed * Time.deltaTime);
    }

    private void AttackMovement()
    {
        Vector3 movementDirection = Player.transform.position - transform.position;
        RotateToMovementDirection(movementDirection);
        if (CurrentAttackCD <= 0)
        {
            Attack();
            CurrentAttackCD = AttackCD;
        }
        else
        {
            CurrentAttackCD -= Time.deltaTime;
        }
    }

    private bool DetectPlayer(float detectionRange)
    {
        return Vector2.Distance(transform.position, Player.transform.position) < detectionRange;
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

    private void Attack()
    {
        GameObject thornMissile = Instantiate(ThornMissilePrefab, transform.position, Quaternion.identity);
        thornMissile.GetComponent<ThornMissileMovement>().SetDirection(Player.transform.position - transform.position);
    }
}