using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public float moveSpeed = 2f;
    public Sprite deadSprite;

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