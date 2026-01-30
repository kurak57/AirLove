using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIManager uIManager;

    [Header("GameSettings")]
    [SerializeField] private float maxHealth = 100f;

    public float MaxHealth()
    {
        return maxHealth;
    }

    public void GetDamage(float amount)
    {
        maxHealth -= amount;
    }
}
