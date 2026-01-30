using UnityEngine;

public class HouseProperties : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public float moveSpeed = 2f;

    [Header("Bump Settings")]
    [SerializeField] private float bumpDistanceX = 0.5f; 
    [SerializeField] private float bumpDistanceY = 0.2f;
    [SerializeField] private int bumpDamage = 1;


    private void Start() {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void HandleCloudCollision(string cloudTag)
    {
        if (cloudTag == "TopCloud")
        {
            transform.Translate(new Vector2(-bumpDistanceX, -bumpDistanceY));
        }
        else if (cloudTag == "BottomCloud")
        {
            transform.Translate(new Vector2(-bumpDistanceX, bumpDistanceY));
        }
        DamageToHouse(bumpDamage);
    }

    public void DamageToHouse(int damage)
    {
        gameManager.HealthDecrease(damage);
    }
    private void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}