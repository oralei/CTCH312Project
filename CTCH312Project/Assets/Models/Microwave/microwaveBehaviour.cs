using UnityEngine;

public class microwaveBehaviour : MonoBehaviour
{
    private Animator mwAnimator;
    private bool isOpen;

    private bool isPizzaInside;
    private bool isHeating;
    private bool isDoneHeating;

    Transform pizzaObject;

    public Material offMat;
    public Material onMat;

    public GameObject objectBody;

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
                            Invoke("heatPizza", 5);
                        }
                        else
                        {
                            isPizzaInside = false;
                            pizzaObject.gameObject.SetActive(false);
                            GameManager.setGameState(20);
                        }
                    }
                    else
                    {
                        // Place pizza inside microwave
                        pizzaObject.gameObject.SetActive(true);
                        isPizzaInside = true;
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
}