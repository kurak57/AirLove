using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // ... (Variabel dan fungsi Awake, OnEnable, OnDisable, Update tidak berubah) ...
    [Header("Movement Settings")]
    [SerializeField] float speed = 10f;
    [SerializeField] float climbSpeed = 5f;

    [Header("Climbing Settings")]
    [Tooltip("Seberapa dekat pemain harus berada dari pusat tangga untuk bisa turun.")]
    [SerializeField] float horizontalClimbThreshold = 0.2f;
    [SerializeField] private Animator _animator;

    [Header("Input Actions")]
    public InputActionReference moveInput;

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private Vector2 move;
    private bool isTouchingStair = false;
    private float originalGravityScale;
    
    private StairController currentStairController; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        originalGravityScale = rb.gravityScale;
    }

    private void OnEnable()
    {
        moveInput.action.Enable();
    }

    private void OnDisable()
    {
        moveInput.action.Disable();
    }

    private void Update()
    {
        move = moveInput.action.ReadValue<Vector2>();

        if (CanDropDown())
        {
            StartCoroutine(DisableCollision(currentStairController.targetPlatform));
        }
    }

    private bool CanDropDown()
    {
        if (currentStairController == null || currentStairController.targetPlatform == null || move.y > -0.1f)
        {
            return false;
        }

        float horizontalDistance = Mathf.Abs(transform.position.x - currentStairController.transform.position.x);
        return horizontalDistance < horizontalClimbThreshold;
    }


    // --- PERUBAHAN UTAMA DI SINI ---
    private void FixedUpdate()
    {
        // Cek kondisi untuk memanjat
        if (isTouchingStair && Mathf.Abs(move.y) > 0.1f)
        {
            // Panggil fungsi memanjat
            Debug.Log($"Cek move.y: {Mathf.Abs(move.y)}");
            Climb();
            // Atur animasi memanjat di sini
            _animator.SetBool("isClimb", true); 
        }
        else
        {
            // Jika tidak memanjat, bergerak horizontal
            MoveHorizontally();
            // Pastikan animasi memanjat dimatikan di sini
            _animator.SetBool("isClimb", false);
        }
    }

    // --- PERUBAHAN DI SINI ---
    private void Climb()
    {
        rb.gravityScale = 0f;
        // P.S. ada typo di kode Anda (linearVelocityX), seharusnya linearVelocity.x atau velocity.x
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, move.y * climbSpeed);
        
        // Logika animator dipindahkan ke FixedUpdate, jadi hapus dari sini.
    }

    private void MoveHorizontally()
    {
        rb.gravityScale = originalGravityScale;
        rb.linearVelocity = new Vector2(move.x * speed, rb.linearVelocity.y);

        // Atur animasi berjalan
        if (move.x != 0)
        {
            _animator.SetBool("isWalking", true);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }
        
        FlipSprite();
    }
    
    // ... (Sisa skrip: FlipSprite, OnTrigger, DisableCollision tidak berubah) ...
    private void FlipSprite()
    {
        if (move.x < -0.01f)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (move.x > 0.01f)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Stair"))
        {
            StairController stair = other.GetComponent<StairController>();
            if (stair != null)
            {
                isTouchingStair = true;
                currentStairController = stair;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Stair") && other.GetComponent<StairController>() == currentStairController)
        {
            isTouchingStair = false;
            currentStairController = null;
            rb.gravityScale = originalGravityScale;
        }
    }

    private IEnumerator DisableCollision(GameObject platformToDisable)
    {
        Collider2D platformCollider = platformToDisable.GetComponent<Collider2D>();
        
        if (platformCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
            yield return new WaitForSeconds(0.5f);
            Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
        }
    }
}