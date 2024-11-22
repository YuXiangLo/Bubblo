using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput Instance;

    public Vector2 Move {get; private set;}
    public bool Jump {get; private set;}
    public bool Fire {get; private set;}
    public bool Interact {get; private set;}
    // Start is called before the first frame update

    private PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _fireAction;
    private InputAction _interactAction;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        _playerInput = GetComponent<PlayerInput>();
        SetupInputAction();
    }

    private void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        Move = _moveAction.ReadValue<Vector2>();
        Jump = _jumpAction.triggered;
        Fire = _fireAction.triggered;
        Interact = _interactAction.triggered;
    }

    private void SetupInputAction()
    {
        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
        _fireAction = _playerInput.actions["Fire"];
        _interactAction = _playerInput.actions["Interact"];
    }
}
