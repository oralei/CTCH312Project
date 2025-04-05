using UnityEngine;

public class brokenVase : MonoBehaviour
{
    static public bool hasBroom;
    public GameObject wallBroom;
    public GameObject handGarbageBag;
    [SerializeField] public GameObject handBroom;
    public GameObject garbageBin;
    public static brokenVase Instance { get; private set; }
    public bool cleanedUpVase = false;
    private AudioManager audioManager;
    public AudioClip throwOutGarbage;
    public AudioClip sweep;
    public int sweepCounter = 0;
    public GameObject garbageArrow;

    public Renderer brokenVaseShards;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cleanUpVase()
    {
        if (sweepCounter < 3)
        {
            audioManager.PlaySFX(sweep);
            sweepCounter++;
        }
        else
        {
            removeBrokenVase();
            handGarbageBag.SetActive(true);
            handBroom.SetActive(false);
            cleanedUpVase = true;
            garbageArrow.SetActive(true);
            garbageBin.layer = LayerMask.NameToLayer("interactableLayer");

        }

    }
    public void getBroom()
    {
        wallBroom.SetActive(false);
        handBroom.SetActive(true);
    }
    public void garbage()
    {
        handGarbageBag.SetActive(false);
        audioManager.PlaySFX(throwOutGarbage);
        GameManager.setGameState(45);
        GameManager.Instance.UpdateTaskText("Talk to Billy");
    }
    private void removeBrokenVase()
    {
        brokenVaseShards.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
    }
}
