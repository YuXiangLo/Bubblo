using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Camera MainCamera;
    private Rigidbody2D Rigidbody2D;

    public float MoveSpeed = 10f;
    public float Gravity = -45f;
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
		IsFloating = Input.GetButton("Jump")  && CanFloat;
	}

    private void HorizontalMovementDetect() {
        var horizontalInput = Input.GetAxis("Horizontal");
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
