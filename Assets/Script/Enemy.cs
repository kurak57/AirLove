using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public float moveSpeed = 2f;

    // Ubah ini menjadi 'Sprite' agar bisa diisi dengan aset gambar dari Project
    public Sprite deadSprite;

    private SpriteRenderer spriteRenderer;
    private bool isDead = false;

    void Start()
    {
        // Ambil komponen SpriteRenderer yang ada di objek ini saat game dimulai
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Jika sudah mati, abaikan semua tabrakan
        if (isDead) return;

        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            health -= 1;

            // Gunakan <= 0 untuk berjaga-jaga jika health jadi minus
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true; // Tandai bahwa musuh sudah mati

        // 1. Ganti sprite dengan sprite mati
        spriteRenderer.sprite = deadSprite;

        // 2. Hentikan gerakan musuh
        moveSpeed = 0f;

        // 3. Matikan collider agar tidak bisa ditabrak atau mentrigger apapun lagi
        GetComponent<Collider2D>().enabled = false;

        // (Opsional) Hancurkan objek musuh setelah 2 detik agar tidak menumpuk di scene
        Destroy(gameObject, 1f);
    }

    private void Update()
    {
        // Hanya bergerak jika belum mati
        if (!isDead)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }
}