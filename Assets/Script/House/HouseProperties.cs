using UnityEngine;

public class HouseProperties : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed = 2f;

    [Header("Bump Settings")]
    public float bumpDistanceX = 0.5f; // Jarak mundur ke kiri
    public float bumpDistanceY = 0.2f; // Jarak mundur ke atas/bawah

    // Fungsi publik ini akan dipanggil oleh skrip WallCollider
    public void HandleWallCollision(string cloudTag)
    {
        // Logika bumping yang sama, sekarang dipicu oleh dinding
        if (cloudTag == "TopCloud")
        {
            Debug.Log($"Dinding menabrak {cloudTag}!");
            // Mundur ke kiri (X negatif) dan ke bawah (Y negatif)
            transform.Translate(new Vector2(-bumpDistanceX, -bumpDistanceY));
        }
        else if (cloudTag == "BottomCloud")
        {
            Debug.Log($"Dinding menabrak {cloudTag}!");
            // Mundur ke kiri (X negatif) dan ke atas (Y positif)
            transform.Translate(new Vector2(-bumpDistanceX, bumpDistanceY));
        }
    }

    private void Update()
    {
        // Terus bergerak ke kanan setiap frame
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}