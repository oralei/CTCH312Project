using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetName : MonoBehaviour
{
    //public TMP_InputField input;
    public GameObject blackScreen;
    public float fadeDuration = 2f;
    public Image fadeImage;

    public prefixedInput prefixedInput;

    [SerializeField] private string mainScene = "MainScene";

    // Switches scene and gets player's name
    public void PlayerName()
    {
        blackScreen.SetActive(true);
        FPSController.playerName = prefixedInput.GetUserInput();
        //SceneManager.LoadScene(mainScene);
        FadeToBlack(fadeDuration);
    }

    public void FadeToBlack(float fadeDuration)
    {
        StartCoroutine(Fade(0f, 1)); // Fade from Transparent to Black
    }

    public void FadeFromBlack(float fadeDuration)
    {
        StartCoroutine(Fade(1, 0f)); // Fade from Black to Transparent
    }

    // Contains copy of fade function
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
        SceneManager.LoadScene(mainScene);
    }
}
