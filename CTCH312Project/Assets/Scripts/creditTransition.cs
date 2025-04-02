using UnityEngine;

public class creditTransition : MonoBehaviour
{
    public AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        Invoke("switchToCredits", 22f);
    }

    
    void Update()
    {
        
    }
    void switchToCredits()
    {
        audioManager.PlaySFX(audioManager.creditMusic);
    }
}
