using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager gameManager;
    [Header("HelathBar")]
    [SerializeField] private Image healthBarImage;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Progress Bar")]
    [SerializeField] private Image progressImage;
    [SerializeField] private Transform house,startpoint ,finishPoint;

    private float initialDistance;

    void Start()
    {
        initialDistance = Vector3.Distance(startpoint.position, finishPoint.position);
        progressImage.fillAmount = 0;
        Debug.Log(initialDistance);
    }
    void  FixedUpdate()
    {
        UpdateProgressionBar();
        UpdateHealthBar();
    }

    public void ChangeHealthText(int healthAmount)
    {
        healthText.text = healthAmount.ToString();
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = gameManager.MaxHealth();
    }

    private void UpdateProgressionBar()
    {
        float distanceHouse = Vector3.Distance(house.position, finishPoint.position);
        float progressAmount = 1f - (distanceHouse / initialDistance);
        progressAmount = Mathf.Max(progressImage.fillAmount, progressAmount);
        progressImage.fillAmount = progressAmount;
    }

}
