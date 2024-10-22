using UnityEngine;

public class ThornMissileMovement : MonoBehaviour
{
    [SerializeField] private float Speed = 9f;
    private Vector3 Direction;

    public void SetDirection(Vector3 direction)
    {
        Direction = direction;
    }

    public void HandleMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Direction, Speed * Time.deltaTime);
    }
}