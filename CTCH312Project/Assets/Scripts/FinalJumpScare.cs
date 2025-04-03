using UnityEngine;
using Yarn.Unity;

public class FinalJumpScare : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject jumpscareTrigger;
    public GameObject doorArrow;
    public AudioSource audioSource;

    public AudioClip bangDing;

    public interactableObject basementDoor;
    public DialogueRunner dialogueRunner;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }

    public void triggerFinalJS()
    {
        audioManager.PlaySFX(audioManager.lockedDoor);
        if (GameManager.Instance.gameEventState == 75)
        {
            Invoke("playDoorBell", 1.23f);
            Invoke("playBangDingOw", 2f);
            Invoke("changeTaskDoorbell", (audioManager.doorbell.length - 5f));
            jumpscareTrigger.SetActive(true);
        }
    }

    private void changeTaskDoorbell()
    {
        GameManager.setGameState(80);
        doorArrow.SetActive(true);
        GameManager.Instance.UpdateTaskText("Answer the door");
    }

    private void playDoorBell()
    {
        audioManager.PlaySFX(audioManager.doorbell);
    }

    private void playBangDingOw()
    {
        audioSource.PlayOneShot(bangDing);
    }
}
