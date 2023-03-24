using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector2 _mousePos;
    private SpriteRenderer _mouseSprite;

    public static PlayerController Instance { get; private set; }
    public static IInteractable PossibleInteractableObject { get; private set; }

    private void SubscribeToInput() 
    { 
        InputManager.OnInteract += OnInteract; 
        InputManager.OnMousePos += OnMousePos;
    }
    private void UnsubscribeToInput() 
    { 
        InputManager.OnInteract -= OnInteract; 
        InputManager.OnMousePos -= OnMousePos;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        SubscribeToInput();
    }

    void Start()
    {
        _mainCamera = Camera.main;
        _mouseSprite = GetComponent<SpriteRenderer>();
        Cursor.visible = false;
    }


    void FixedUpdate()
    {
        transform.position = _mousePos;
    }


    private void OnMousePos(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _mousePos = context.ReadValue<Vector2>();
            _mousePos = _mainCamera.ScreenToWorldPoint(_mousePos);

            _mousePos.x = _mousePos.x + ( _mouseSprite.size.x * transform.localScale.x /2);
            _mousePos.y = _mousePos.y - ( _mouseSprite.size.y * transform.localScale.y /2);
           
            _mousePos.x = Mathf.Clamp(_mousePos.x, -13f, 13.5f);
            _mousePos.y = Mathf.Clamp(_mousePos.y, -10.2f, 9.5f);
        }
    }


    private void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InteractWithObject(PossibleInteractableObject);
        }
    }


    private void InteractWithObject(IInteractable interactableObject)
    {
        if(interactableObject != null)
        {
            interactableObject?.Interact();
        }
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        PossibleInteractableObject = other.GetComponent<IInteractable>();
    }


    private void OnTriggerExit2D() 
    {
        PossibleInteractableObject = null;
    }


    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
        UnsubscribeToInput();
    }
}