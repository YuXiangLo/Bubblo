using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput Instance;

    // Input values
    public Vector2 Move { get; private set; }
    public bool Jump { get; private set; }
    public bool Fire { get; private set; }
    public bool Interact { get; private set; }

    // Continuous input (like GetKey)
    public bool IsJumpHeld => _jumpAction.ReadValue<float>() > 0;
    public bool IsFireHeld => _fireAction.ReadValue<float>() > 0;

    private PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _fireAction;
    private InputAction _interactAction;

    private bool _jumpPressedThisFrame;
    private bool _firePressedThisFrame;
    private bool _interactPressedThisFrame;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _playerInput = GetComponent<PlayerInput>();
        SetupInputAction();
    }

    private void Update()
    {
        // Reset "pressed this frame" flags
        Jump = _jumpPressedThisFrame;
        Fire = _firePressedThisFrame;
        Interact = _interactPressedThisFrame;

        _jumpPressedThisFrame = false;
        _firePressedThisFrame = false;
        _interactPressedThisFrame = false;

        // Update continuous input
        Move = _moveAction.ReadValue<Vector2>();
    }

    private void SetupInputAction()
    {
        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
        _fireAction = _playerInput.actions["Fire"];
        _interactAction = _playerInput.actions["Interact"];

        // Handle "pressed this frame" logic
        _jumpAction.performed += ctx => { _jumpPressedThisFrame = true; };
        _fireAction.performed += ctx => { _firePressedThisFrame = true; };
        _interactAction.performed += ctx => { _interactPressedThisFrame = true; };

        // Reset "held" flags on key release
        _jumpAction.canceled += ctx => Jump = false;
        _fireAction.canceled += ctx => Fire = false;
        _interactAction.canceled += ctx => Interact = false;
    }
}
