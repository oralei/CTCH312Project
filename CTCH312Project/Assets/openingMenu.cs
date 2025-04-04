using UnityEngine;

public class openingMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    AudioManager audioManager;
    public AudioClip openingSound;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlaySFX(openingSound);
        Invoke("toggleCursor", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void toggleCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
