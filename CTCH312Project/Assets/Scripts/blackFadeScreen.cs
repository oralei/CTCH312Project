using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class blackFadeScreen : MonoBehaviour
{
    public float fadeDuration = 1.5f;
    public Image fadeImage;

    void Start()
    {
        FadeFromBlack(fadeDuration);
    }

    public void FadeToBlack(float fadeDuration)
    {
        StartCoroutine(Fade(0f, 1)); // Fade from Transparent to Black
    }

    public void FadeFromBlack(float fadeDuration)
    {
        StartCoroutine(Fade(1, 0f)); // Fade from Black to Transparent
    }

    // Changes the alpha of an image overtime
    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }
}