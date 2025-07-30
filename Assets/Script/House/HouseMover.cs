using System;
using UnityEngine;
using UnityEngine.InputSystem;

// SCRIPT INI DIPASANG DI OBJEK RUMAH YANG MEMILIKI RIGIDBODY2D
public class HouseMover : MonoBehaviour
{
    [Header("Pengaturan Gerakan")]
    [Tooltip("Input Action untuk pergerakan vertikal (atas/bawah).")]
    private InputActionReference activeMoveAction;
    [Tooltip("Kecepatan gerakan rumah.")]
    public float moveSpeed = 3f;

    private Rigidbody2D rb; // Variabel untuk menyimpan komponen Rigidbody2D
    private float verticalInput = 0f;

    // Awake dipanggil sebelum Start
    void Awake()
    {
        // Dapatkan komponen Rigidbody2D dari objek ini
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
        // Hentikan gerakan saat script dinonaktifkan
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

    // FixedUpdate digunakan untuk semua pembaruan terkait fisika
    void FixedUpdate()
    {
        if (rb == null) return;

        // Buat vektor kecepatan baru berdasarkan input
        // Kita tidak perlu Time.deltaTime saat mengatur velocity secara langsung
        Vector2 targetVelocity = new Vector2(0, verticalInput * moveSpeed);
        
        // Atur kecepatan Rigidbody2D
        rb.linearVelocity = targetVelocity;
    }
}