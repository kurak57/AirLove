using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIManager uIManager;
    [SerializeField] private ScreenFlash screenFlash;

    [Header("GameSettings")]
    public float maxHealth {get; private set;} = 100f;
    private float currentHealth;
    private bool gameOver;

    public bool GameOver => gameOver;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public float CurrentHealth()
    {
        return currentHealth;
    }

    public void HealthDecrease(float amount)
    {
        screenFlash.FlashRed();
        currentHealth -= amount;
    }
}
