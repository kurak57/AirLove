using UnityEngine;
using UnityEngine.InputSystem;

public class RopeShooterRotation : MonoBehaviour
{
    [Header("Pengaturan Input")]
    [Tooltip("Input Action untuk pergerakan (gunakan sumbu Y dari Vector2).")]
    public InputActionReference activeRotationAction;

    [Header("Pengaturan Rotasi")]
    [Tooltip("Kecepatan rotasi dalam derajat per detik.")]
    public float rotationSpeed = 90f;

    // Menambahkan SerializeField agar bisa diatur dari Inspector
    [Header("Batasan Rotasi (Derajat)")]
    [SerializeField]
    [Tooltip("Batas rotasi minimum dalam derajat. Contoh: -60")]
    private float minRotationAngle = -30f;

    [SerializeField]
    [Tooltip("Batas rotasi maksimum dalam derajat. Contoh: 60")]
    private float maxRotationAngle = 45f;

    // Variabel privat untuk melacak input dan rotasi
    private float rotationInput = 0f;
    private float rotationMultiplier = 1f;
    private float currentZRotation = 0f; // Variabel baru untuk menyimpan rotasi saat ini

    void Awake()
    {
        // Cek jika objek ini punya parent
        if (transform.parent != null)
        {
            // Jika nama parent-nya adalah "BottomShooter", balik arah rotasi
            if (transform.parent.name == "Top")
            {
                Debug.Log("Parent adalah BottomShooter, rotasi akan dibalik.");
                rotationMultiplier = -1f;
            }
        }
    }

    public void SetRotationAction(InputActionReference rotationAction)
    {
        this.activeRotationAction = rotationAction;
    }
    private void OnEnable()
    {
        activeRotationAction.action.Enable();
        activeRotationAction.action.performed += OnMove;
        activeRotationAction.action.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        activeRotationAction.action.Disable();
        activeRotationAction.action.performed -= OnMove;
        activeRotationAction.action.canceled -= OnMoveCanceled;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>().y;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        rotationInput = 0f;
    }

    // --- LOGIKA UTAMA ADA DI SINI ---
    void Update()
    {
        // 1. Hitung berapa besar perubahan rotasi pada frame ini
        float rotationAmount = rotationInput * rotationSpeed * rotationMultiplier * Time.deltaTime;

        // 2. Tambahkan perubahan rotasi ke total rotasi saat ini
        currentZRotation += rotationAmount;

        // 3. Batasi (clamp) nilai rotasi agar tidak melebihi batas min dan max
        currentZRotation = Mathf.Clamp(currentZRotation, minRotationAngle, maxRotationAngle);

        // 4. Terapkan rotasi yang sudah dibatasi ke object
        // Menggunakan localRotation lebih aman jika object memiliki parent
        transform.localRotation = Quaternion.Euler(0f, 0f, currentZRotation);
    }
}