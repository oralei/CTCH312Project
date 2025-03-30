using UnityEngine;

public class PhoneJS : MonoBehaviour
{
    AudioManager audioManager;
    public AudioSource audioSource;

    public bool ready = false;

    public static PhoneJS Instance { get; private set; }

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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ready)
        {
            ringPhone();
        }
    }

    void ringPhone()
    {
        audioSource.PlayOneShot(audioManager.telephone);
        ready = false;
    }
}
