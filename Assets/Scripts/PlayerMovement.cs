using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float MoveSpeed = 10f;
    public float Gravity = -45f;
    public float FloatingYSpeed = -2f;
    public float JumpForce = 20f;
    public float PlayerSize = 1f; 
	public float FloatingRatio = 0.9995f;
	public float TriggerDistance = 0.001f;

    public bool _isGrounded = false;
    public bool _isFloating = false;
	public bool _canFloat = false;
	public float FloatingXSpeed = 10f;
    public Vector2 _velocity = Vector2.zero;
    private Camera _mainCamera;
    private Rigidbody2D _rigidbody2D;
    
    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
		_rigidbody2D.freezeRotation = true;
        _mainCamera = Camera.main;
    }

    // private void Update() { NOTE: Since I modularize the PlayerMovement, this script therefore would not call the Update() itself but by Player.cs
	public void HandleMovement() {
		_isGrounded = _rigidbody2D.Raycast(Vector2.down, new Vector2(PlayerSize, PlayerSize), TriggerDistance);
		FloatingMovementDetect();
		HorizontalMovementDetect();
		if (_isGrounded) 
			JumpMovementDetect();
		ApplyGravity();
		RestrictPlayerWithinCamera();
	}

	private void FixedUpdate() {
		_rigidbody2D.MovePosition(_rigidbody2D.position + _velocity * Time.fixedDeltaTime);
		FloatingXSpeed = _isGrounded ? MoveSpeed : FloatingXSpeed;
	}

	private void FloatingMovementDetect() {
		if (_velocity.y < 0f && Input.GetButtonDown("Jump"))
			_canFloat = true;
		_isFloating = Input.GetButton("Jump")  && _canFloat;
	}

    private void HorizontalMovementDetect() {
        var horizontalInput = Input.GetAxis("Horizontal");
        float multiplier = _isFloating ? FloatingRatio : 1f;
		FloatingXSpeed *= multiplier;
        _velocity.x = horizontalInput * FloatingXSpeed;
    }

    private void JumpMovementDetect() {
        _velocity.y = Mathf.Max(_velocity.y, 0f);
        if (Input.GetButtonDown("Jump")) {
            _velocity.y = JumpForce;
			_canFloat = false;
        }
    }

    private void ApplyGravity() {
        if (_isFloating && _velocity.y < FloatingYSpeed)
            _velocity.y = FloatingYSpeed;
        else 
            _velocity.y += Gravity * Time.deltaTime;
    }

    private void RestrictPlayerWithinCamera() {
        float cameraHalfWidth = _mainCamera.orthographicSize * _mainCamera.aspect;
        float cameraLeftEdge = _mainCamera.transform.position.x - cameraHalfWidth;
        float cameraRightEdge = _mainCamera.transform.position.x + cameraHalfWidth;

        Vector2 playerPosition = transform.position;
        playerPosition.x = Mathf.Clamp(playerPosition.x, cameraLeftEdge + PlayerSize / 2, cameraRightEdge - PlayerSize / 2);
        transform.position = playerPosition;
    }
}
