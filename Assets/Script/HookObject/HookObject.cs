using UnityEngine;

public class HookObject : MonoBehaviour
{

    [SerializeField] private Alphabet letterType;
    [SerializeField] private Sprite sprite;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public Alphabet LetterType()
    {
        return letterType;
    }
    
}




