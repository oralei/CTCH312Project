using UnityEngine;
using Yarn.Unity;

public class stateChanger : MonoBehaviour
{
    public GameObject brokenVase;
    public GameObject vase;

    public BillyMovement BillyMovement;
    public DialogueRunner dialogueRunner;
    public YarnFunctions YarnFunctions;
    public interactableObject interactableObject;

    AudioManager audioManager;
    public AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Activates broken vase on volume enter
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the "Player" tag
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.gameEventState == 25)
            {
                GameManager.setGameState(GameManager.Instance.gameEventState + 5);

                brokenVase.SetActive(true);
                audioSource.PlayOneShot(audioManager.vase);
                vase.SetActive(false);
                BillyMovement.MoveToDestination(new Vector3(-4.58400011f, 0.134000003f, -4.94799995f), Quaternion.Euler(0, 90, 0));

                interactableObject.OnDialogueStart();
                dialogueRunner.StartDialogue("brokenVaseNode");

                GameManager.Instance.UpdateTaskText("Investigate the noise");
            }
            else
                Debug.Log("Already changed");
        }
    }
}
