using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Camera MainCamera;
    private Rigidbody2D Rigidbody2D;

    public float MoveSpeed = 8f;
    public float Gravity = -90f;
	public float FloatingXSpeed = 10f;
    public float FloatingYSpeed = -2f;
    public float JumpForce = 18f;
    public float PlayerSize = 1f;
	public float FloatingRatio = 0.9995f;
	public float TriggerDistance = 0.001f;
    public float DefaultGravityScale = 1f;
    public float LowGravityScale = 0.5f;

    private float GravityScale;
    private bool IsGrounded = false;
    private bool IsFloating = false;
	private bool CanFloat = false;
    public Vector2 Velocity = Vector2.zero;
    
    private void Awake() {
        Rigidbody2D = GetComponent<Rigidbody2D>();
		Rigidbody2D.freezeRotation = true;
        MainCamera = Camera.main;
    }

    // private void Update() { NOTE: Since I modularize the PlayerMovement, this script therefore would not call the Update() itself but by Player.cs
	public void HandleMovement() {
		IsGrounded = Rigidbody2D.Raycast(Vector2.down, new Vector2(PlayerSize, PlayerSize), TriggerDistance);
		FloatingMovementDetect();
		HorizontalMovementDetect();
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
		IsFloating = Input.GetButton("Jump") && CanFloat;

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

    private void ApplyGravity() {
        if (IsFloating && Velocity.y < FloatingYSpeed)
            Velocity.y = FloatingYSpeed;
        else 
            Velocity.y += Gravity * GravityScale * Time.deltaTime;
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
