using UnityEngine;

public class WallCollider : MonoBehaviour
{
    private HouseProperties houseProperties;

    const string enemyTag = "Enemy";
    void Start()
    {
        houseProperties = GetComponentInParent<HouseProperties>();
        if (houseProperties == null)
        {
            Debug.LogError("WallCollider tidak dapat menemukan HouseProperties!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TopCloud") || other.CompareTag("BottomCloud"))
        {
            houseProperties.HandleCloudCollision(other.tag);
        }

        Debug.Log(other.name);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            int enemyDamage = enemy.GetDamageAmount();
            houseProperties.DamageToHouse(enemyDamage);
        }

    }
}