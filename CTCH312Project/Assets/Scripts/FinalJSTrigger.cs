using UnityEngine;

public class FinalJSTrigger : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject scaryMan;

    public blackFadeScreen blackFadeScreen;

    public interactableObject interactableObject;

    public AudioClip scare1;
    public AudioClip scare2;
    AudioClip scare3;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactableObject.OnDialogueStart();
            scaryMan.SetActive(true);
            audioManager.PlaySFX(scare1);
            audioManager.PlaySFX(scare2);

            Invoke("fadeAfter", 1.6f);

            Debug.Log("boo!");
        }
    }

    private void fadeAfter()
    {
        blackFadeScreen.FadeToBlack(1f);
    }
}
