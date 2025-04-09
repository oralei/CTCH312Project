using UnityEngine;

public class GhostJumpScare : MonoBehaviour
{
    public DoorController door;
    public GameObject ghost;
    public GameObject hotdog;
    public GameObject normalRoom;
    public GameObject normalLight;

    public bool ready = false;

    AudioManager audioManager;

    public static GhostJumpScare Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Closes bedroom door, activates ghost, and removes furniture 
    public void setupJS()
    {
        // close the door if its open
        if (door.isOpening == true)
            door.isOpening = false;

        door.speed = 10f;

        // Disable normal stuff
        normalLight.SetActive(false);
        normalRoom.SetActive(false);

        // Enable JS stuff
        ghost.SetActive(true);
        hotdog.SetActive(true);

        ready = true;
    }

    public void triggerGhostJS()
    {
        Debug.Log("ghost boo!");
        audioManager.PlaySFX(audioManager.ghostJumpscare);
        Invoke("jumpscareReturn", 0.44f);
    }

    // Returns room to normal state
    public void jumpscareReturn()
    {
        // enable normal stuff
        normalLight.SetActive(true);
        normalRoom.SetActive(true);

        // disable JS stuff
        ghost.SetActive(false);

        door.speed = 5f;
    }
}
