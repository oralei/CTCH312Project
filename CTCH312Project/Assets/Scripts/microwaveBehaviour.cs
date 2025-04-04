using UnityEngine;
using Yarn;
using Yarn.Unity;

public class microwaveBehaviour : MonoBehaviour
{
    public static microwaveBehaviour Instance;

    private Animator mwAnimator;
    private bool isOpen;

    static public bool hasPizza;
    [SerializeField] public GameObject handPizza;

    private bool isPizzaInside;
    private bool isHeating;
    private bool isDoneHeating;

    Transform pizzaObject;

    public Material offMat;
    public Material onMat;

    public GameObject objectBody;

    public DialogueRunner dialogueRunner;
    public YarnFunctions YarnFunctions;

    AudioManager audioManager;
    public AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent <AudioManager>();
    }

    void Start()
    {
        mwAnimator = GetComponent<Animator>();
        pizzaObject = gameObject.transform.Find("insidePizza");

        // too lazy to add closed on start?
        mwAnimator.SetTrigger("closeTrig");
        isOpen = false;
    }

    void Update()
    {
        
    }

    public void openMicrowave()
    {
        if (mwAnimator != null)
        {
            if (isOpen)
            {
                // Check if we are ready to heat pizza (game state):
                if (GameManager.Instance.gameEventState == 15)
                {
                    if (isPizzaInside)
                    {
                        if (!isDoneHeating) // initalize
                        {
                            // we have to check if pizza is heating, if so dont allow open!
                            closeDoor();
                            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
                            Debug.Log("Current Layer: " + LayerMask.LayerToName(gameObject.layer));
                            Debug.Log("Starting to heat pizza...");
                            objectBody.GetComponent<Renderer>().material = onMat;
                            audioSource.PlayOneShot(audioManager.microwaveHeat);
                            Invoke("heatPizza", 5);
                        }
                        else // pizza is done heating
                        {
                            audioManager.PlaySFX(audioManager.squish);
                            isPizzaInside = false;
                            handPizza.SetActive(true);
                            pizzaObject.gameObject.SetActive(false);
                            GameManager.Instance.UpdateTaskText("Serve food to Billy");
                            GameManager.setGameState(20);
                        }
                    }
                    else
                    {
                        // Place pizza inside microwave
                        if(hasPizza == true)
                        {
                            pizzaObject.gameObject.SetActive(true);
                            handPizza.SetActive(false);
                            isPizzaInside = true;
                        }
                        else
                        {
                            TriggerOneLineDialogue("I need the pizza first");
                        }

                    }
                }
                else
                    closeDoor();
            }
            else
                openDoor();
        }
    }

    // Heat pizza, change to pickupable:
    public void heatPizza()
    {
        Debug.Log("Pizza is done heating!");
        audioSource.PlayOneShot(audioManager.microwaveBeep);
        isDoneHeating = true;

        objectBody.GetComponent<Renderer>().material = offMat;
        gameObject.layer = LayerMask.NameToLayer("interactableLayer");
    }

    public void openDoor()
    {
        mwAnimator.SetTrigger("openTrig");
        isOpen = true;
    }

    public void closeDoor()
    {
        mwAnimator.SetTrigger("closeTrig");
        isOpen = false;
    }

    private void TriggerOneLineDialogue(string line)
    {
        // Run the dialogue
        YarnFunctions.storage.SetValue("$interactMsg", line);
        dialogueRunner.StartDialogue("InteractObject");
    }
}