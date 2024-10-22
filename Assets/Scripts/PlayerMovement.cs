using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float MoveSpeed = 8f;
	public float FloatingXSpeed = 10f;
    public float FloatingYSpeed = -2f;
    public float Gravity = -90f;
    public float MaxFallingSpeed = -20f;
    public float JumpForce = 18f;
    public float PlayerSize = 1f; 
	public float FloatingRatio = 0.9995f;
	public float TriggerDistance = 0.001f;
	public bool IsFacingRight = true;
    public bool IsGrounded = false;
    public float DefaultGravityScale = 1f;
    public float LowGravityScale = 0.5f;
	public Vector2 Velocity = Vector2.zero;

    private float GravityScale;
    private Camera MainCamera;
    private Rigidbody2D Rigidbody2D;

	[SerializeField] private float KnockbackForce = 6f;
	[SerializeField] private bool IsFloating = false;
	[SerializeField] private bool CanFloat = false;
	[SerializeField] private bool DisableHorizontalMovement = false;
	[SerializeField] private float KnockbackTangent = 2f;

    public float Speed {get => Mathf.Abs(Velocity.x);}

    private void Awake() {
        Rigidbody2D = GetComponent<Rigidbody2D>();
		Rigidbody2D.freezeRotation = true;
        MainCamera = Camera.main;
    }

    // Movement logic
    public void HandleMovement() {
		IsGrounded = Rigidbody2D.Raycast(Vector2.down, new Vector2(PlayerSize, PlayerSize), TriggerDistance);
		FloatingMovementDetect();
		if (!DisableHorizontalMovement)
			HorizontalMovementDetect();
		if (IsGrounded) 
			JumpMovementDetect();
		FaceSideDetect();
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
        GravityScale =  Input.GetButton("Jump") ? LowGravityScale : DefaultGravityScale;
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

	private void FaceSideDetect() {
		if (Velocity.x > 0 && !IsFacingRight || Velocity.x < 0 && IsFacingRight) {
			IsFacingRight = !IsFacingRight;
			Vector3 currentScale = transform.localScale;
			currentScale.x *= -1;
			transform.localScale = currentScale;
		}
	}

    private void ApplyGravity() {
        if (IsFloating && Velocity.y < FloatingYSpeed)
            Velocity.y = FloatingYSpeed;
        else 
            Velocity.y += Gravity * GravityScale * Time.deltaTime;
        
        // Limit Falling Speed
        Velocity.y = Mathf.Max(Velocity.y, MaxFallingSpeed);
    }

    private void RestrictPlayerWithinCamera() {
        float cameraHalfWidth = MainCamera.orthographicSize * MainCamera.aspect;
        float cameraLeftEdge = MainCamera.transform.position.x - cameraHalfWidth;
        float cameraRightEdge = MainCamera.transform.position.x + cameraHalfWidth;

        Vector2 playerPosition = transform.position;
        playerPosition.x = Mathf.Clamp(playerPosition.x, cameraLeftEdge + PlayerSize / 2, cameraRightEdge - PlayerSize / 2);
        transform.position = playerPosition;
    }

    public void BubbleJump() {
        Velocity.y = JumpForce;
    }

	public void Knockback(Vector2 knockbackDirection, float toSleep) {
		StartCoroutine(KnockbackCoroutine(knockbackDirection, toSleep));
	}

	private IEnumerator KnockbackCoroutine(Vector2 knockbackDirection, float toSleep) {
		DisableHorizontalMovement = true;
		Velocity.x = (knockbackDirection.x > 0) ? -KnockbackForce : KnockbackForce;
		Velocity.y = KnockbackTangent * KnockbackForce;

		yield return new WaitForSeconds(toSleep);
		DisableHorizontalMovement = false;
	}
}
