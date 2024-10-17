using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public GameObject lightProjectilePrefab;
    public GameObject mediumProjectilePrefab;
    public GameObject heavyProjectilePrefab;

    public float lightProjectileSpeed = 10f;
    public float mediumProjectileSpeed = 7f;
    public float heavyProjectileSpeed = 3f;
    public float lifetime = 2f;

    private float lightAttackThreshold = 0.2f;
    private float mediumAttackThreshold = 1f;

    private float buttonHoldTime = 0f;
    private bool isButtonHeld = false;

    private Rigidbody2D playerRigidbody;

    private void Awake() {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Handles projectile instantiation and movement
    private void ThrowProjectile(GameObject projectilePrefab, float projectileSpeed)
    {
        GameObject projectile = Instantiate(projectilePrefab, new Vector2(playerRigidbody.position.x + 1f, playerRigidbody.position.y), Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = transform.right * projectileSpeed;
        }
        Destroy(projectile, lifetime);
    }

    public void LightAttack()
    {
        Debug.Log("Performed Light Attack");
        ThrowProjectile(lightProjectilePrefab, lightProjectileSpeed);
    }

    public void MediumAttack()
    {
        Debug.Log("Performed Medium Attack");
        ThrowProjectile(mediumProjectilePrefab, mediumProjectileSpeed);
    }

    public void HeavyAttack()
    {
        Debug.Log("Performed Heavy Attack");
        ThrowProjectile(heavyProjectilePrefab, heavyProjectileSpeed);
    }

    // Method to be called in the PlayerMovement class
    public void AttackDetect()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            buttonHoldTime = 0f;
            isButtonHeld = true;
        }
        if (Input.GetButton("Fire1") && isButtonHeld)
        {
            buttonHoldTime += Time.deltaTime;
        }
        if (Input.GetButtonUp("Fire1") && isButtonHeld)
        {
            isButtonHeld = false;

            if (buttonHoldTime < lightAttackThreshold)
            {
                LightAttack();
            }
            else if (buttonHoldTime < mediumAttackThreshold)
            {
                MediumAttack();
            }
            else
            {
                HeavyAttack();
            }

            buttonHoldTime = 0f;
        }
    }
}
