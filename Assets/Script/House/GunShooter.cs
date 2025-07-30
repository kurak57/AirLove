// GunShooter.cs (MODIFIED)
using UnityEngine;
using UnityEngine.InputSystem;

public class GunShooter : MonoBehaviour
{
    [Header("Referensi")]
    [Tooltip("Prefab peluru yang akan ditembakkan.")]
    public GameObject projectilePrefab;
    [Tooltip("Titik di mana peluru akan muncul.")]
    public Transform firePoint;

    [Header("Pengaturan Tembakan")]
    [Tooltip("Kecepatan peluru.")]
    public float projectileSpeed = 20f;
    [Tooltip("Jarak peluru.")]
    public float projectileTimeSpan = 1f;

    // Variabel untuk menyimpan referensi input yang diberikan secara dinamis
    private InputActionReference activeFireAction;
    private float directionMultiplier = 1f;

    void Awake()
    {
        if (transform.parent != null && transform.parent.name == "BottomShooter")
        {
            Debug.Log("Parent adalah BottomShooter, arah tembakan akan dibalik.");
            directionMultiplier = -1f;
        }
    }

    // Metode publik untuk mengatur action mana yang harus digunakan
    public void SetFireAction(InputActionReference fireAction)
    {
        this.activeFireAction = fireAction;
    }

    private void OnEnable()
    {
        // Berlangganan ke action yang sudah di-set sebelumnya
        if (activeFireAction != null)
        {
            activeFireAction.action.performed += OnFire;
            activeFireAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        // Berhenti berlangganan untuk membersihkan
        if (activeFireAction != null)
        {
            activeFireAction.action.performed -= OnFire;
            activeFireAction.action.Disable();
        }
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        
        if (rb != null)
        {
            Vector2 fireDirection = firePoint.right * directionMultiplier;
            rb.linearVelocity = fireDirection * projectileSpeed;
        }
        
        Destroy(projectile, projectileTimeSpan);
    }
}