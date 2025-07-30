using UnityEngine;

public class WallCollider : MonoBehaviour
{
    // Referensi ke skrip utama di objek induk (House)
    private HouseProperties houseController;

    void Start()
    {
        // Secara otomatis mencari skrip HouseProperties pada objek induk
        houseController = GetComponentInParent<HouseProperties>();
        if (houseController == null)
        {
            Debug.LogError("WallCollider tidak dapat menemukan HouseProperties di induknya!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Cek tag objek yang bersentuhan
        if (other.CompareTag("TopCloud") || other.CompareTag("BottomCloud"))
        {
            // Jika bersentuhan dengan awan, panggil fungsi di skrip utama
            // dan kirimkan tag dari awan tersebut
            houseController.HandleWallCollision(other.tag);
        }
    }
}