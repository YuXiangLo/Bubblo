using UnityEngine;

public class ThornMissile : MonoBehaviour
{
    [SerializeField] private float Damage = 15f;
    private ThornMissileMovement ThornMissileMovement;

    private void Awake()
    {
        ThornMissileMovement = GetComponent<ThornMissileMovement>();
        ThornMissileMovement.SetDirection(GameObject.Find("Player").transform.position);
    }

    private void Update()
    {
        ThornMissileMovement.HandleMovement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IModifyHealth>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}