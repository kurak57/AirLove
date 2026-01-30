using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private UIManager uIManager;

    public List<LetterData> letterDatas = new List<LetterData>();
    [SerializeField] private string word;
    // [SerializeField] private GameObject letterPrefab;

    private Dictionary<Alphabet, Sprite> letterDict;

    void Awake()
    {
        letterDict = new Dictionary<Alphabet, Sprite>();

        foreach (var letterData in letterDatas)
        {
            letterDict[letterData.letter] = letterData.sprite;
        }
    }
    public Sprite GetSprite(Alphabet targetLetter)
    {
        letterDict.TryGetValue(targetLetter, out Sprite sprite);
        return sprite;
    }

    public string ReturnCurrentWord()
    {
        word = word.ToUpper();
        return word;
    }

    public void ChecklistObjectHooked(GameObject gameObject)
    {
        List<GameObject> objectList = uIManager.letterObjects;
        Debug.Log(objectList);
        HookObject hookedObject = gameObject.GetComponent<HookObject>();

        foreach (var objectUI in objectList)
        {
            LetterUI lettertype = objectUI.GetComponent<LetterUI>();

            if (lettertype.letterType == hookedObject.LetterType())
            {
                Image letterImage = objectUI.GetComponent<Image>();
                uIManager.SetImageAlpha(letterImage, 1);
            }
        }
    }
}