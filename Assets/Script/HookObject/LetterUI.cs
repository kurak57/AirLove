using UnityEngine;

public class LetterUI : MonoBehaviour
{
    public Alphabet letterType {get; private set;}

    public void SetLetterType(Alphabet type)
    {
        letterType = type;
    }
}
