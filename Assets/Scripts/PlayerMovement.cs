using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour {
    private Camera MainCamera;
    private Rigidbody2D Rigidbody2D;

    public float MoveSpeed = 10f;
    public float Gravity = -60f;
    public float FloatingYSpeed = -2f;
    public float JumpForce = 20f;
    public float PlayerSize = 1f; 
	public float FloatingRatio = 0.9995f;
	public float TriggerDistance = 0.001f;

    public bool IsGrounded = false;
    public bool IsFloating = false;
	public bool CanFloat = false;
	public float FloatingXSpeed = 10f;
    public Vector2 Velocity = Vector2.zero;

    // For Attack
    public GameObject lightProjectilePrefab;
    public GameObject mediumProjectilePrefab;
    public GameObject heavyProjectilePrefab;
    private float lightAttackThreshold = 0.2f;
    private float mediumAttackThreshold = 1f;
    public float lightProjectileSpeed = 10f;
    public float mediumProjectileSpeed = 7f;
    public float heavyProjectileSpeed = 3f;
    public float lifetime = 2f;
    private float buttonHoldTime = 0f; // Track how long the button is held
    private bool isButtonHeld = false;
    
    
    private void Awake() {
        Rigidbody2D = GetComponent<Rigidbody2D>();
		Rigidbody2D.freezeRotation = true;
        MainCamera = Camera.main;
    }

    private void ThrowProjectile(GameObject projectilePrefab, float projectileSpeed)
    {
        // Instantiate the projectile at the fire point
        GameObject projectile = Instantiate(projectilePrefab, new Vector2(Rigidbody2D.position.x + 1f, Rigidbody2D.position.y), Quaternion.identity);

        // Add force to the projectile's Rigidbody2D to make it move
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = transform.right * projectileSpeed;  // Assuming the player is facing right
        }
        Destroy(projectile, lifetime);
    }

    private void LightAttack()
    {
        UnityEngine.Debug.Log("Performed Light Attack");
        ThrowProjectile(lightProjectilePrefab, lightProjectileSpeed);
    }

    private void MediumAttack()
    {
        UnityEngine.Debug.Log("Performed Medium Attack");
        ThrowProjectile(mediumProjectilePrefab, mediumProjectileSpeed);
    }

    private void HeavyAttack()
    {
        UnityEngine.Debug.Log("Performed Heavy Attack");
        ThrowProjectile(heavyProjectilePrefab, heavyProjectileSpeed);
    }

    private void AttackDetect() {
        if(Input.GetButtonDown("Fire1")){
            buttonHoldTime = 0f;
            isButtonHeld = true;
        }
        if (Input.GetButton("Fire1") && isButtonHeld){
            buttonHoldTime += Time.deltaTime;
        }
        if(Input.GetButtonUp("Fire1") && isButtonHeld){
            isButtonHeld = false;

            if (buttonHoldTime < lightAttackThreshold){
                LightAttack();
            }else if (buttonHoldTime < mediumAttackThreshold){
                MediumAttack();
            }else{
                HeavyAttack();
            }

            buttonHoldTime = 0f;
        }
    }

    // private void Update() { NOTE: Since I modularize the PlayerMovement, this script therefore would not call the Update() itself but by Player.cs
	public void HandleMovement() {
		IsGrounded = Rigidbody2D.Raycast(Vector2.down, new Vector2(PlayerSize, PlayerSize), TriggerDistance);
		FloatingMovementDetect();
		HorizontalMovementDetect();
        AttackDetect();
		if (IsGrounded) 
			JumpMovementDetect();
		ApplyGravity();
		RestrictPlayerWithinCamera();
	}

	private void FixedUpdate() {
		Rigidbody2D.MovePosition(Rigidbody2D.position + Velocity * Time.fixedDeltaTime);
		FloatingXSpeed = IsGrounded ? MoveSpeed : FloatingXSpeed;
	}

	private void FloatingMovementDetect() {
		if (Velocity.y < 0f && Input.GetButtonDown("Jump"))
			CanFloat = true;
		IsFloating = Input.GetButton("Jump")  && CanFloat;
	}

    private void HorizontalMovementDetect() {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        float multiplier = IsFloating ? FloatingRatio : 1f;
		FloatingXSpeed *= multiplier;
        Velocity.x = horizontalInput * FloatingXSpeed;
    }

    private void JumpMovementDetect() {
        Velocity.y = Mathf.Max(Velocity.y, 0f);
        if (Input.GetButtonDown("Jump")) {
            Velocity.y = JumpForce;
			CanFloat = false;
        }
    }

    private void ApplyGravity() {
        if (IsFloating && Velocity.y < FloatingYSpeed)
            Velocity.y = FloatingYSpeed;
        else 
            Velocity.y += Gravity * Time.deltaTime;
    }

    private void RestrictPlayerWithinCamera() {
        float cameraHalfWidth = MainCamera.orthographicSize * MainCamera.aspect;
        float cameraLeftEdge = MainCamera.transform.position.x - cameraHalfWidth;
        float cameraRightEdge = MainCamera.transform.position.x + cameraHalfWidth;

        Vector2 playerPosition = transform.position;
        playerPosition.x = Mathf.Clamp(playerPosition.x, cameraLeftEdge + PlayerSize / 2, cameraRightEdge - PlayerSize / 2);
        transform.position = playerPosition;
    }
}
