using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class RunningJS : MonoBehaviour
{
    AudioManager audioManager;
    public AudioSource audioSource;

    public Vector3 moveDirection = Vector3.right; // Move direction (default is right)
    public float speed = 2f; // Speed of movement

    public bool ready = true;
    private bool isMoving = false; // Flag to track movement state

    public GameObject runningGhost;

    public static RunningJS Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            runningGhost.transform.position += moveDirection * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ready)
        {
            triggerRunningJS();
        }
    }

    void triggerRunningJS()
    {
        Debug.Log("Boo!");
        //audioSource.PlayOneShot(audioManager.telephone);

        runningGhost.SetActive(true);
        StartCoroutine(MoveAndStop(2));
        audioSource.PlayOneShot(audioManager.stringsHit);

        ready = false;
    }

    // Coroutine to handle movement and stopping after delay
    private IEnumerator MoveAndStop(float seconds)
    {
        isMoving = true; // Start moving

        yield return new WaitForSeconds(seconds); // Wait for the specified time

        isMoving = false; // Stop moving
    }
}

