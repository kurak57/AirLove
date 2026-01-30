using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite deadSprite;
    [Header("Properties")]
    [SerializeField] private int health = 3;
    [SerializeField] private int damage = 1;
    [SerializeField] private float moveSpeed = 2f;

    private SpriteRenderer spriteRenderer;
    private bool isDead = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            health -= 1;

            if (health <= 0)
            {
                Die();
            }
        }
    }

    public int GetDamageAmount()
    {
        return damage;
    }

    void Die()
    {
        isDead = true;
        spriteRenderer.sprite = deadSprite;
        moveSpeed = 0f;

        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1f);
    }

    private void Update()
    {
        if (!isDead)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }
}