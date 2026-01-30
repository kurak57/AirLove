using UnityEngine;

public class WallCollider : MonoBehaviour
{
    private HouseProperties houseController;

    void Start()
    {
        houseController = GetComponentInParent<HouseProperties>();
        if (houseController == null)
        {
            Debug.LogError("WallCollider tidak dapat menemukan HouseProperties di induknya!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TopCloud") || other.CompareTag("BottomCloud"))
        {
            houseController.HandleWallCollision(other.tag);
        }
    }
}