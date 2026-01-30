using UnityEngine;

public class HouseProperties : MonoBehaviour
{
    // public int health = 100;
    public float moveSpeed = 2f;

    [Header("Bump Settings")]
    public float bumpDistanceX = 0.5f; 
    public float bumpDistanceY = 0.2f;

    public void HandleWallCollision(string cloudTag)
    {
        if (cloudTag == "TopCloud")
        {
            transform.Translate(new Vector2(-bumpDistanceX, -bumpDistanceY));
        }
        else if (cloudTag == "BottomCloud")
        {
            transform.Translate(new Vector2(-bumpDistanceX, bumpDistanceY));
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}