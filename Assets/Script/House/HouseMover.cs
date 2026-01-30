using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HouseMover : MonoBehaviour
{
    [Header("Move Setting")]
    private InputActionReference activeMoveAction;
    public float moveSpeed = 3f;

    private Rigidbody2D rb; 
    private float verticalInput = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Komponen Rigidbody2D tidak ditemukan di objek ini!", this);
        }
    }

    public void SetMoveAction(InputActionReference moveAction)
    {
        this.activeMoveAction = moveAction;
    }

    void OnEnable()
    {
        activeMoveAction.action.Enable();
        activeMoveAction.action.performed += OnMove;
        activeMoveAction.action.canceled += OnMoveCanceled;
    }

    void OnDisable()
    {
        verticalInput = 0f;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        activeMoveAction.action.Disable();
        activeMoveAction.action.performed -= OnMove;
        activeMoveAction.action.canceled -= OnMoveCanceled;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        verticalInput = context.ReadValue<Vector2>().y;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        verticalInput = 0f;
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        Vector2 targetVelocity = new Vector2(0, verticalInput * moveSpeed);

        rb.linearVelocity = targetVelocity;
    }
}