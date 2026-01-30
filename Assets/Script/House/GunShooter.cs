using UnityEngine;
using UnityEngine.InputSystem;

public class GunShooter : MonoBehaviour
{
    [Header("references")]
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Shooter Settings")]
    public float projectileSpeed = 20f;
    public float projectileTimeSpan = 1f;

    private InputActionReference activeFireAction;
    private float directionMultiplier = 1f;

    void Awake()
    {
        if (transform.parent != null && transform.parent.name == "BottomShooter")
        {
            directionMultiplier = -1f;
        }
    }

    public void SetFireAction(InputActionReference fireAction)
    {
        this.activeFireAction = fireAction;
    }

    private void OnEnable()
    {
        if (activeFireAction != null)
        {
            activeFireAction.action.performed += OnFire;
            activeFireAction.action.Enable();
        }
    }

    private void OnDisable()
    {
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