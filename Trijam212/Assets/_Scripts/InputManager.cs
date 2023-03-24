using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private PlayerInput _playerInput;
    public static event Action<CallbackContext> OnMousePos;
    public static event Action<CallbackContext> OnInteract;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _playerInput = GetComponent<PlayerInput>();
        SubscribeToInput();
        DontDestroyOnLoad(gameObject);
    }


    private void OnMousePosInput(CallbackContext context)
    {
        OnMousePos?.Invoke(context);
    }


    private void OnInteractInput(CallbackContext context)
    {
        OnInteract?.Invoke(context);
    }


    private void SubscribeToInput()
    {
        _playerInput.actions["MousePos"].started += OnMousePosInput;
        _playerInput.actions["MousePos"].performed += OnMousePosInput;
        _playerInput.actions["MousePos"].canceled += OnMousePosInput;

        _playerInput.actions["Interact"].started += OnInteractInput;
        _playerInput.actions["Interact"].performed += OnInteractInput;
        _playerInput.actions["Interact"].canceled += OnInteractInput;
    }

    private void UnsubscribeFromInput()
    {
        _playerInput.actions["MousePos"].started -= OnMousePosInput;
        _playerInput.actions["MousePos"].performed -= OnMousePosInput;
        _playerInput.actions["MousePos"].canceled -= OnMousePosInput;

        _playerInput.actions["Interact"].started -= OnInteractInput;
        _playerInput.actions["Interact"].performed -= OnInteractInput;
        _playerInput.actions["Interact"].canceled -= OnInteractInput;
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
            UnsubscribeFromInput();
        }
    }
}