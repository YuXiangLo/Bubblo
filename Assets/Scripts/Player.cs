using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // player move speed
    public float MoveSpeed = 6f;

    // The acceleration of the gravity
    public float Gravity = -45f;

    // floating speed
    public float FloatingSpeed = -2f;

    // the velocity going up when the player presses jump
    public float JumpForce = 15f;

    // player size
    public float PlayerSize = 0.5f;

	public float FloatingRatio = 0.8f;

	public float TriggerDistance = 0.1f;

    [SerializeField] float _currentBlood, _maxBlood = 10f;
    public BloodBar _bloodBar;
    public Vector2 _velocity = Vector2.zero;
    public bool _isGrounded = false;
    public bool _isFloating = false;
    public Rigidbody2D _rigidbody2D;
    public Camera _mainCamera;
    

    // Start is called before the first frame update
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
		_rigidbody2D.freezeRotation = true;
        _bloodBar = GetComponentInChildren<BloodBar>();
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        _bloodBar.UpdateBloodBar(_currentBlood, _maxBlood);
    }

    // Update is called once per frame
    private void Update()
    {
        _isGrounded = _rigidbody2D.Raycast(Vector2.down, new Vector2(PlayerSize, PlayerSize), TriggerDistance);
        _isFloating = Input.GetButton("Jump") && !_isGrounded;

        HorizontalMovementDetect();

        if (_isGrounded) 
        {
            JumpMovementDetect();
        }

        ApplyGravity();
        RestrictPlayerWithinCamera();
    }

    private void FixedUpdate() 
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _velocity * Time.fixedDeltaTime);
    }

    private void HorizontalMovementDetect()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        float multiplier = _isFloating ? FloatingRatio : 1f;

        _velocity.x = horizontalInput * MoveSpeed * multiplier;
    }

    private void JumpMovementDetect()
    {
        // Keep the vertical speed to 0 when on the ground.
        _velocity.y = Mathf.Max(_velocity.y, 0f);

        if (Input.GetButtonDown("Jump"))
        {
            _velocity.y = JumpForce;
        }
    }

    private void ApplyGravity() 
    {
        if (_isFloating && _velocity.y < 0f)
        {
            _velocity.y = FloatingSpeed;
        }
        else 
        {
            _velocity.y += Gravity * Time.deltaTime;
        }
    }

    private void RestrictPlayerWithinCamera()
    {
        // Get the camera's boundaries in world space
        float cameraHalfWidth = _mainCamera.orthographicSize * _mainCamera.aspect;
        float cameraLeftEdge = _mainCamera.transform.position.x - cameraHalfWidth;
        float cameraRightEdge = _mainCamera.transform.position.x + cameraHalfWidth;

        // Get the player's current position
        Vector2 playerPosition = transform.position;

        // Clamp the player's x position so they don't go outside the camera's view
        playerPosition.x = Mathf.Clamp(playerPosition.x, cameraLeftEdge + PlayerSize, cameraRightEdge - PlayerSize);

        // Apply the clamped position back to the player
        transform.position = playerPosition;
    }

    private void TakeDamage(float damageCount)
    {
        _currentBlood -= damageCount;
        _bloodBar.UpdateBloodBar(_currentBlood, _maxBlood);
        if (_currentBlood <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {

    }

    // void OnCollisionEnter2D(Collision2D collision2D)
    // {

    // }
}
