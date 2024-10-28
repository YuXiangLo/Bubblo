using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // player movement
    public float GravityScale;
    public bool IsFacingRight = true;
    public bool IsGrounded = false;
    public bool IsFloating = false;
	public bool CanFloat = false;
	public bool DisableHorizontalMovement = false;
    public Vector2 Velocity = Vector2.zero;
    public float Speed {get => Mathf.Abs(Velocity.x);}
    public IPlayerMovementState PlayerMovementState;
    public IPlayerAttackState PlayerAttackState;

    private PlayerData PlayerData;
    private Rigidbody2D Rigidbody2D;
    private PlayerHealth PlayerHealth;
    private Camera MainCamera;

    public void Heal(float amount) {
        PlayerHealth.Heal(amount);
    }

    public void TakeDamage(float amount) {
        PlayerHealth.TakeDamage(amount);
    }

    public void BubbleJump()
    {
        PlayerMovementState.BubbleJump();
    }

    public void Knockback(Vector2 knockbackDirection, float toSleep) {
        PlayerMovementState = new PlayerMovementKnockBackState(this, PlayerData, knockbackDirection, toSleep);
		PlayerMovementState.HandleMovement();
	}

    private void Start()
    {
        PlayerData = GetComponent<PlayerData>();
        PlayerHealth = GetComponent<PlayerHealth>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
		Rigidbody2D.freezeRotation = true;
        PlayerMovementState = new PlayerMovementInitialState(this, PlayerData);
        MainCamera = Camera.main;
    }

    private void Update()
    {
        DetectPlayerStatus();
        HandleMovement();
        DetectFaceSide();
        RestrictPlayerWithinCamera();
    }

    private void FixedUpdate()
    {
        Rigidbody2D.MovePosition(Rigidbody2D.position + Velocity * Time.fixedDeltaTime);
    }

    private void DetectPlayerStatus()
    {
        IsGrounded = Rigidbody2D.Raycast(Vector2.down, new Vector2(PlayerData.PlayerSize, PlayerData.PlayerSize), PlayerData.TriggerDistance);
		GravityScale =  Input.GetButton("Jump") ? PlayerData.LowGravityScale : PlayerData.DefaultGravityScale;
    }

    private void HandleMovement()
    {
        PlayerMovementState.HandleMovement();
    }

    private void DetectFaceSide() {
		if (Velocity.x > 0 && !IsFacingRight 
                || Velocity.x < 0 && IsFacingRight) {
			IsFacingRight = !IsFacingRight;
			Vector3 currentScale = transform.localScale;
			currentScale.x *= -1;
			transform.localScale = currentScale;
		}
	}

    private void RestrictPlayerWithinCamera() {
        float cameraHalfWidth = MainCamera.orthographicSize * MainCamera.aspect;
        float cameraLeftEdge = MainCamera.transform.position.x - cameraHalfWidth;
        float cameraRightEdge = MainCamera.transform.position.x + cameraHalfWidth;

        Vector2 playerPosition = transform.position;
        playerPosition.x = Mathf.Clamp(playerPosition.x, cameraLeftEdge + PlayerData.PlayerSize / 2, cameraRightEdge - PlayerData.PlayerSize / 2);
        transform.position = playerPosition;
    }
}
