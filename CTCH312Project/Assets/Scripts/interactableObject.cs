using System.Linq;
using TMPro;
using UnityEngine;
using Yarn;
using Yarn.Unity;
using static FPSController;
using System;
using System.Collections.Generic; // Required for List<T>

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

                            // Pizza is heated and ready to give to Billy:
                            case 20:
                                dialogueRunner.StartDialogue("feedBillyNode");
                                break;

                            case 25:
                                dialogueRunner.StartDialogue("washroomNode");
                                break;

                            case 30:
                                dialogueRunner.StartDialogue("brokenVaseNode");
                                break;

                            case 35:
                                dialogueRunner.StartDialogue("brokenVaseNode");
                                break;

                            case 40:
                                OnDialogueStart();
                                TriggerOneLineDialogue("Let's clean this mess up first...");
                                break;

                            case 45:
                                dialogueRunner.StartDialogue("letsPlayNode");
                                break;

                            case 50:
                                dialogueRunner.StartDialogue("letsPlayNode");
                                break;

                            default:
                                Debug.Log("DANGER: No case for state " + GameManager.Instance.gameEventState);
                                GameObject.FindWithTag("Player").GetComponent<FPSController>().OnDialogueEnd();
                                break;
                        }
                    }
                }
                else{
                    OnDialogueStart();
                    TriggerOneLineDialogue("Let's talk to the parents first...");
                }
                break;

            // Door interaction:
            case "door":
                DoorController doorController = GetComponent<DoorController>();
                doorController.isOpening = !doorController.isOpening;
                break;
            
            case "fruit":
                exploreCountObject("fruit");
                gameObject.SetActive(false);
                break;

            case "pizza":
                if(GameManager.Instance.gameEventState == 15)
                {
                    microwaveBehaviour.hasPizza = true;
                    Destroy(GameObject.Find("fridgePizza"));
                }
                Debug.Log("This is a pizza slice!");
                break;

            case "brokenVase":
                if (GameManager.Instance.gameEventState == 30)
                {
                    OnDialogueStart();
                    TriggerOneLineDialogue("Seems like the kid broke it...");
                    GameManager.setGameState(35);
                }
                else if (GameManager.Instance.gameEventState == 35)
                {
                    TriggerOneLineDialogue("Let's ask Billy what happened.");
                }
                else if (GameManager.Instance.gameEventState == 40)
                {
                    // Clean vase:
                    Destroy(GameObject.Find("brokenVase"));
                    GameManager.setGameState(45);
                    GameManager.Instance.UpdateTaskText("Talk to Billy");
                }
                break;

            case "phone":
                exploreCountObject("phone");
                OnDialogueStart();
                TriggerOneLineDialogue("That's an old telephone.");
                break;

            case "coffee":
                exploreCountObject("coffee");
                OnDialogueStart();
                TriggerOneLineDialogue("I could use a cup of coffee right now...");
                break;

            case "soap":
                exploreCountObject("soap");
                OnDialogueStart();
                TriggerOneLineDialogue("Don't drop it!");
                break;

            case "bookcase":
                exploreCountObject("bookcase");
                OnDialogueStart();
                TriggerOneLineDialogue("Was never much of a reader...");
                break;

            case "tv":
                exploreCountObject("tv");
                OnDialogueStart();
                TriggerOneLineDialogue("Kids really watch this stuff?");
                break;

            case "magazines":
                exploreCountObject("magazines");
                OnDialogueStart();
                TriggerOneLineDialogue("Who left these here?");
                break;

            case "microwave":
                gameObject.GetComponent<microwaveBehaviour>().openMicrowave();
                break;

            case "vase":
                exploreCountObject("vase");
                OnDialogueStart();
                TriggerOneLineDialogue("This vase looks wobbly...");
                break;

            case "chicken":
                exploreCountObject("chicken");
                OnDialogueStart();
                TriggerOneLineDialogue("I guess the chicken crossed the road...");
                break;

            case "coke":
                exploreCountObject("coke");
                OnDialogueStart();
                TriggerOneLineDialogue("I prefer Pepsi...");
                break;

            case "pie":
                exploreCountObject("pie");
                OnDialogueStart();
                TriggerOneLineDialogue("I can feel myself floating from the smell...");
                break;

            case "hotdog":
                exploreCountObject("hotdog");
                OnDialogueStart();
                TriggerOneLineDialogue("A glizzy?");
                break;

            case "bigFan":
                exploreCountObject("bigFan");
                OnDialogueStart();
                TriggerOneLineDialogue("This is my biggest fan!");
                break;

            case "remote":
                OnDialogueStart();
                TriggerOneLineDialogue("I probably shouldn't change the channel...");
                Debug.Log("remote");
                break;

            case "object1":
                exploreCountObject("object1");
                Debug.Log("Object in array after: " + GameManager.Instance.objectsFound.Count);
                break;

            case "object2":
                exploreCountObject("object2");
                Debug.Log("Object in array after: " + GameManager.Instance.objectsFound.Count);
                break;

            case "object3":
                exploreCountObject("object3");
                Debug.Log("Object in array after: " + GameManager.Instance.objectsFound.Count);
                break;

            default:
                Debug.Log("Default interaction.");
                break;
        }
    }

    public void OnDialogueStart()
    {
        // Disable player movement
        GameObject.FindWithTag("Player").GetComponent<FPSController>().canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void TriggerOneLineDialogue(string line)
    {
        if (!dialogueRunner.IsDialogueRunning)
        {
            // Run the dialogue
            YarnFunctions.storage.SetValue("$interactMsg", line);
            dialogueRunner.StartDialogue("InteractObject");
        }
    }

    private void exploreCountObject(string objectID)
    {
        if (GameManager.Instance.gameEventState >= 10)
        {
            if (!GameManager.Instance.objectsFound.Contains(objectID))
            {
                GameManager.Instance.objectsFound.Add(objectID);
                Debug.Log("Added new object.");
            }
            else
            {
                Debug.Log("Not a new object.");
            }

            if (GameManager.Instance.objectsFound.Count == 3)
            {
                GameManager.setGameState(15);
                OnDialogueStart();
                dialogueRunner.StartDialogue("feedBillyNode");
            }
        }
    }
}
