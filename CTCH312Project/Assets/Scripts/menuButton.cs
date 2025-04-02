using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI theText;

    AudioManager audioManager;

    public AudioClip hoverSound;

    private Vector3 originalScale;
    [SerializeField] private float hoverScale = 1.2f;
    [SerializeField] private float duration = 0.2f;

    [SerializeField] private float clickScale = 0.9f;

    private Button button;

    public blackFadeScreen blackFadeScreen;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        originalScale = transform.localScale;

        button = GetComponent<Button>();

        // Ensure the button is interactable
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClicked);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = Color.yellow; //Or however you do your color
        transform.DOScale(originalScale * hoverScale, duration).SetEase(Ease.OutQuad);
        audioManager.PlaySFX(hoverSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = Color.white; //Or however you do your color
        transform.DOScale(originalScale, duration).SetEase(Ease.OutQuad);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnButtonClicked();
    }

    private void OnButtonClicked()
    {
        transform.DOScale(originalScale * clickScale, 0.1f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => transform.DOScale(originalScale * hoverScale, 0.1f).SetEase(Ease.OutQuad));

        audioManager.PlaySFX(audioManager.buttonPress);
    }
    
    public void PlayGame()
    {
        blackFadeScreen.FadeToBlack(1f);
        Invoke("ChangeScene", 3f);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}