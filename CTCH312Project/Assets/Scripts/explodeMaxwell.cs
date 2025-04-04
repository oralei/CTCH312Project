using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class explodeMaxwell : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    AudioManager audioManager;
    public AudioSource audioSource;

    public int explodeCounter = 0;

    public float additivePitch = 1.0f;

    Renderer objectRenderer;

    [Header("Growth Animation Settings")]
    [SerializeField] private float growthAmount = 0.1f;
    [SerializeField] private float animationDuration = 0.2f;
    [SerializeField] private AnimationCurve growthCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    private Vector3 originalScale;
    private Coroutine currentAnimation;
    private bool isAnimating = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioSource = transform.GetComponent<AudioSource>();
        objectRenderer = transform.GetComponent<Renderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = transform.localScale;
        spriteRenderer.enabled = false;
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode() 
    {
        // Cancel any running animation first
        if (isAnimating && currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        // Start new animation
        currentAnimation = StartCoroutine(PetAnimation());

        if (explodeCounter <= 4)
        {
            audioSource.PlayOneShot(audioManager.meow);
            explodeCounter++;
        }
        else if (explodeCounter == 5)
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(audioManager.meow);
            explodeCounter++;
        }
        else if (explodeCounter <= 24)
        {
            additivePitch = additivePitch + 0.05f;
            audioSource.pitch = additivePitch;
            audioSource.PlayOneShot(audioManager.meow);
            growthAmount += 0.01f;
            explodeCounter++;
        }
        else
        {
            audioManager.PlaySFX(audioManager.explode);
            objectRenderer.enabled = false;
            int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
            gameObject.layer = LayerIgnoreRaycast;
            PlayAnimationOnce();
        }
    }

    public void PlayAnimationOnce()
    {
        // Make the sprite visible
        spriteRenderer.enabled = true;

        // Play the animation
        animator.Rebind();
        animator.enabled = true;
        animator.Play("YourAnimationName");

        // Calculate how long to wait before hiding
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

        // Schedule hiding after animation completes
        Invoke("HideSprite", animationLength);
    }

    void HideSprite()
    {
        // Hide the sprite after animation completes
        spriteRenderer.enabled = false;
    }

    private IEnumerator PetAnimation()
    {
        isAnimating = true;

        // Grow/bounce animation
        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            float curveValue = growthCurve.Evaluate(t);

            // Calculate scale with bounce/growth effect
            Vector3 newScale = originalScale * (1f + (growthAmount * curveValue));
            transform.localScale = newScale;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure we return to original scale
        transform.localScale = originalScale;
        isAnimating = false;
    }
}
