using UnityEngine;
using System.Collections;

public class CameraFade : MonoBehaviour
{
    public float speedScale = 1f;
    public Color fadeColor = Color.black;
    public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 1),
        new Keyframe(0.5f, 0.5f, -1.5f, -1.5f), new Keyframe(1, 0));
    public bool startFadedOut = false;

    [Tooltip("Higher values appear above lower values. Set this lower than your UI canvas order.")]
    public int sortingOrder = -100;

    private float alpha = 0f;
    private Canvas fadeCanvas;
    private UnityEngine.UI.Image fadeImage;
    private bool isFading = false;
    private bool isFadedOut = false;

    public delegate void FadeCompleteDelegate();
    public event FadeCompleteDelegate OnFadeComplete;

    private void Awake()
    {
        // Create canvas and image
        CreateFadeCanvas();

        // Set initial state
        isFadedOut = startFadedOut;

        if (startFadedOut)
        {
            // Start fully black
            alpha = 1f;
        }
        else
        {
            // Start fully clear
            alpha = 0f;
        }

        UpdateFadeAlpha(alpha);

        FadeOut();
    }

    private void CreateFadeCanvas()
    {
        GameObject canvasObject = new GameObject("FadeCanvas");
        canvasObject.transform.SetParent(transform);

        fadeCanvas = canvasObject.AddComponent<Canvas>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        fadeCanvas.sortingOrder = sortingOrder;

        UnityEngine.UI.CanvasScaler scaler = canvasObject.AddComponent<UnityEngine.UI.CanvasScaler>();
        scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        canvasObject.AddComponent<CanvasGroup>();

        GameObject imageObject = new GameObject("FadeImage");
        imageObject.transform.SetParent(canvasObject.transform, false);

        fadeImage = imageObject.AddComponent<UnityEngine.UI.Image>();
        fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);

        RectTransform rectTransform = fadeImage.rectTransform;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.anchoredPosition = Vector2.zero;
    }

    private void UpdateFadeAlpha(float newAlpha)
    {
        alpha = newAlpha;
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
        }
    }

    /// <summary>
    /// Fades the screen from clear to black
    /// </summary>
    public void FadeOut()
    {
        if (isFadedOut || isFading) return;

        StartCoroutine(FadeRoutine(0f, 1f));
    }

    /// <summary>
    /// Fades the screen from black to clear
    /// </summary>
    public void FadeIn()
    {
        if (!isFadedOut || isFading) return;

        StartCoroutine(FadeRoutine(1f, 0f));
    }

    /// <summary>
    /// Toggles between fade in and fade out
    /// </summary>
    public void ToggleFade()
    {
        if (isFading) return;

        if (isFadedOut)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }

    private IEnumerator FadeRoutine(float startAlpha, float targetAlpha)
    {
        isFading = true;
        float timeElapsed = 0f;

        // Calculate duration based on how far we need to move
        float duration = Mathf.Abs(targetAlpha - startAlpha) / speedScale;

        // Ensure some minimum duration
        duration = Mathf.Max(duration, 0.1f);

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(timeElapsed / duration);

            // If fading to black (out), we use t directly, if fading to clear (in), we invert t for the curve
            float curveTime = (targetAlpha > startAlpha) ? normalizedTime : 1f - normalizedTime;
            float newAlpha = Curve.Evaluate(curveTime);

            UpdateFadeAlpha(newAlpha);
            yield return null;
        }

        // Ensure we end at exactly the target value
        UpdateFadeAlpha(targetAlpha);

        isFadedOut = (targetAlpha >= 0.99f);
        isFading = false;

        if (OnFadeComplete != null)
        {
            OnFadeComplete.Invoke();
        }
    }

    /// <summary>
    /// Sets the screen to be completely clear without animation
    /// </summary>
    public void SetClear()
    {
        StopAllCoroutines();
        isFading = false;
        isFadedOut = false;
        UpdateFadeAlpha(0f);
    }

    /// <summary>
    /// Sets the screen to be completely black without animation
    /// </summary>
    public void SetBlack()
    {
        StopAllCoroutines();
        isFading = false;
        isFadedOut = true;
        UpdateFadeAlpha(1f);
    }
}