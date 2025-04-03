using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class FinalJSTrigger : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject scaryMan;
    public GameObject lookAtPosition;

    private bool isLookingAt = false;

    public FPSController fpsController;

    public blackFadeScreen blackFadeScreen;

    public interactableObject interactableObject;

    public AudioClip scare1;
    public AudioSource audioSource;

    [SerializeField] PlayableDirector timeline;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (isLookingAt)
        {
            lookAt();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fpsController = GameObject.Find("Player").GetComponent<FPSController>();
            fpsController.runSpeed = 0.5f;
            fpsController.canMoveMouse = false;
            isLookingAt = true;
            scaryMan.SetActive(true);

            timeline.Play();
            Invoke("playSound", 0.14f);

            Invoke("fadeAfter", 1.2f);
            Invoke("sceneChange", 6.8f);
            Debug.Log("boo!");
        }
    }

    private void fadeAfter()
    {
        blackFadeScreen.FadeToBlack(1f);
    }
    private void sceneChange()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void lookAt()
    {
        fpsController.playerCamera.transform.LookAt(lookAtPosition.transform.position);
    }

    private void playSound()
    {
        audioSource.PlayOneShot(scare1);
    }
}
