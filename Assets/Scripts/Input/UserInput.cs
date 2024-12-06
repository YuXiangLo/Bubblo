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
    public bool Right { get; private set; }
    public bool Left { get; private set; }
    public bool Up { get; private set; }
    public bool Down { get; private set; }

    // Continuous input (like GetKey)
    public bool IsJumpHeld => _jumpAction.ReadValue<float>() > 0;
    public bool IsFireHeld => _fireAction.ReadValue<float>() > 0;

    private PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _fireAction;
    private InputAction _interactAction;
    private InputAction _rightAction;
    private InputAction _leftAction;
    private InputAction _upAction;
    private InputAction _downAction;

    private bool _jumpPressedThisFrame;
    private bool _firePressedThisFrame;
    private bool _interactPressedThisFrame;
    private bool _rightPressedThisFrame;
    private bool _leftPressedThisFrame;
    private bool _upPressedThisFrame;
    private bool _downPressedThisFrame;

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
        Right = _rightPressedThisFrame;
        Left = _leftPressedThisFrame;
        Up = _upPressedThisFrame;
        Down = _downPressedThisFrame;

        _jumpPressedThisFrame = false;
        _firePressedThisFrame = false;
        _interactPressedThisFrame = false;
        _rightPressedThisFrame = false;
        _leftPressedThisFrame = false;
        _upPressedThisFrame = false;
        _downPressedThisFrame = false;

        // Update continuous input
        Move = _moveAction.ReadValue<Vector2>();
    }

    private void SetupInputAction()
    {
        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
        _fireAction = _playerInput.actions["Fire"];
        _interactAction = _playerInput.actions["Interact"];
        _rightAction = _playerInput.actions["Right"];
        _leftAction = _playerInput.actions["Left"];
        _upAction = _playerInput.actions["Up"];
        _downAction = _playerInput.actions["Down"];

        // Handle "pressed this frame" logic
        _jumpAction.performed += ctx => { _jumpPressedThisFrame = true; };
        _fireAction.performed += ctx => { _firePressedThisFrame = true; };
        _interactAction.performed += ctx => { _interactPressedThisFrame = true; };
        _rightAction.performed += ctx => { _rightPressedThisFrame = true; };
        _leftAction.performed += ctx => { _leftPressedThisFrame = true; };
        _upAction.performed += ctx => { _upPressedThisFrame = true; };
        _downAction.performed += ctx => { _downPressedThisFrame = true; };

        // Reset "held" flags on key release
        _jumpAction.canceled += ctx => Jump = false;
        _fireAction.canceled += ctx => Fire = false;
        _interactAction.canceled += ctx => Interact = false;
        _rightAction.canceled += ctx => Right = false;
        _leftAction.canceled += ctx => Left = false;
        _upAction.canceled += ctx => Up = false;
        _downAction.canceled += ctx => Down = false;
    }
}
