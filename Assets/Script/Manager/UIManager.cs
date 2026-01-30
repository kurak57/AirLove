using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelManager levelManager;
    [Header("HelathBar")]
    [SerializeField] private Image healthBarImage;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Progress Bar")]
    [SerializeField] private Image progressImage;
    [SerializeField] private Transform house,startpoint ,finishPoint;

    [Header("Letter UI")]
    [SerializeField] private GameObject letteruiPrefab;
    [SerializeField] private Transform letterParent;
    public List<GameObject> letterObjects = new List<GameObject>();

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;

    private float initialDistance;

    void Awake()
    {
    }
    void Start()
    {
        GenerateLetterUI();
        initialDistance = Vector3.Distance(startpoint.position, finishPoint.position);
        progressImage.fillAmount = 0;
        Debug.Log(initialDistance);
    }
    void  FixedUpdate()
    {
        UpdateProgressionBar();
        UpdateHealthBar();
    }

    // =========== Health Bar Function ==========
    public void ChangeHealthText(float healthAmount)
    {
        healthText.text = healthAmount.ToString();
    }

     private void UpdateHealthBar()
    {
        float maxHealth = gameManager.maxHealth;
        float currentHealth = gameManager.CurrentHealth();
        healthBarImage.fillAmount = currentHealth/maxHealth;
        ChangeHealthText(currentHealth);
    }
    // =========== Letter Display Function ==========
    private void GenerateLetterUI()
    {
        string thisLevelWord = levelManager.ReturnCurrentWord();

        foreach (var letter in thisLevelWord)
        {
            Alphabet targetEnum = (Alphabet)Enum.Parse(typeof(Alphabet), letter.ToString());
            GameObject letterObject = Instantiate(letteruiPrefab, letterParent);
            SetImage(targetEnum, letterObject);
            SetType(targetEnum, letterObject);
            letterObjects.Add(letterObject);
        }
    }

    private void SetType(Alphabet targetEnum, GameObject letterObject)
    {
        LetterUI letterUI = letterObject.GetComponent<LetterUI>();
        letterUI.SetLetterType(targetEnum);
    }

    private void SetImage(Alphabet targetEnum, GameObject letterObject)
    {
        Image letterImage = letterObject.GetComponent<Image>();
        letterImage.sprite = levelManager.GetSprite(targetEnum);
        SetImageAlpha(letterImage, 0.5f);
    }

    public void SetImageAlpha(Image img, float alpha)
{
    Color c = img.color;
    c.a = alpha;
    img.color = c;
}

    // =========== progress Bar Function ==========
    private void UpdateProgressionBar()
    {
        float distanceHouse = Vector3.Distance(house.position, finishPoint.position);
        float progressAmount = 1f - (distanceHouse / initialDistance);
        progressAmount = Mathf.Max(progressImage.fillAmount, progressAmount);
        progressImage.fillAmount = progressAmount;
    }

}
