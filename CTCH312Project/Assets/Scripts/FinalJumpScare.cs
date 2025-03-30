using UnityEngine;

public class FinalJumpScare : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject jumpscareTrigger;
    public AudioSource audioSource;

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
            audioManager.PlaySFX(audioManager.doorbell);
            jumpscareTrigger.SetActive(true);
        }
    }
}
