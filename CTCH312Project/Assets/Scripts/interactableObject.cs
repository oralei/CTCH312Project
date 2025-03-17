using TMPro;
using UnityEngine;
using Yarn;
using Yarn.Unity;
using static FPSController;

public class interactableObject : MonoBehaviour, IInteractable
{
    public DialogueRunner dialogueRunner;
    public YarnFunctions YarnFunctions;
    public string objectID; // Unique identifier for different interactable objects


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        Debug.Log($"Interacted with: {objectID}");

        switch (objectID)
        {
            case "Mom":
                if (!dialogueRunner.IsDialogueRunning)
                {
                    OnDialogueStart();
                    dialogueRunner.StartDialogue("Start");
                }
                break;

            case "Billy":
                if (GameManager.Instance.gameEventState >= 5)
                {
                    if (!dialogueRunner.IsDialogueRunning)
                    {
                        OnDialogueStart();

                        switch (GameManager.Instance.gameEventState)
                        {
                            case 5:
                                dialogueRunner.StartDialogue("MeetBilly");
                                break;

                            case 10:
                                dialogueRunner.StartDialogue("MeetBilly");
                                break;

                            case 15:
                                dialogueRunner.StartDialogue("feedBillyNode");
                                break;

                            case 20:
                                dialogueRunner.StartDialogue("feedBillyNode");
                                break;

                            default:
                                Debug.Log("DANGER: No case for state " + GameManager.Instance.gameEventState);
                                GameObject.FindWithTag("Player").GetComponent<FPSController>().OnDialogueEnd();
                                break;
                        }
                    }
                }
                else{
                    TriggerOneLineDialogue("Let's talk to the parents first...");
                }
                break;

            case "Appa":
                Debug.Log("Mmmm. Apple.");
                Destroy(gameObject);
                OnDialogueStart();
                GameManager.setGameState(15);
                dialogueRunner.StartDialogue("feedBillyNode");
                break;

            case "pizza":
                Debug.Log("This is a pizza slice!");
                break;

            case "microwave":
                gameObject.GetComponent<microwaveBehaviour>().openMicrowave();
                break;

            case "vase":
                OnDialogueStart();
                TriggerOneLineDialogue("This vase looks wobbly...");
                break;

            default:
                Debug.Log("Default interaction.");
                break;
        }
    }

    private void OnDialogueStart()
    {
        // Disable player movement
        GameObject.FindWithTag("Player").GetComponent<FPSController>().canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /*private void OnDialogueEnd()
    {
        Debug.Log("Dialogue has finished!");

        // Re-enable player movement
        GameObject.FindWithTag("Player").GetComponent<FPSController>().canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }*/

    private void TriggerOneLineDialogue(string line)
    {
        // Run the dialogue
        YarnFunctions.storage.SetValue("$interactMsg", line);
        dialogueRunner.StartDialogue("InteractObject");
    }
}
