using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFlash : MonoBehaviour
{
    [SerializeField] private Image damageOverlay;
    [SerializeField] private float flashDuration = 0.2f;
    [SerializeField] private float maxAlpha = 0.6f;


    Coroutine flashRoutine;

    public void FlashRed()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        damageOverlay.gameObject.SetActive(true);
        Color c = damageOverlay.color;
        c.a = 0f;
        damageOverlay.color = c;

        // Fade in
        float t = 0f;
        while (t < flashDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, maxAlpha, t / flashDuration);
            damageOverlay.color = c;
            yield return null;
        }

        // Fade out
        t = 0f;
        while (t < flashDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(maxAlpha, 0f, t / flashDuration);
            damageOverlay.color = c;
            yield return null;
        }

        c.a = 0f;
        damageOverlay.color = c;
        flashRoutine = null;
    }
}
