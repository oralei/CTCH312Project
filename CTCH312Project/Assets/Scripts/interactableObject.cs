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
    public List<string> objectsFound = new List<string>();


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
                Debug.Log("Object in array: " + objectsFound.Count);
                exploreCountObject("Appa", objectsFound);
                Debug.Log("Object in array after: " + objectsFound.Count);
                Destroy(gameObject);
                break;

            case "pizza":
                if(GameManager.Instance.gameEventState == 15)
                {
                    microwaveBehaviour.hasPizza = true;
                    Destroy(GameObject.Find("fridgePizza"));
                }
                Debug.Log("This is a pizza slice!");
                break;

            case "microwave":
                gameObject.GetComponent<microwaveBehaviour>().openMicrowave();
                break;

            case "vase":
                OnDialogueStart();
                TriggerOneLineDialogue("This vase looks wobbly...");
                break;

            case "object1":
                Debug.Log("Object in array: " + objectsFound.Count);
                exploreCountObject("object1", objectsFound);
                Debug.Log("Object in array after: " + objectsFound.Count);
                break;

            case "object2":
                Debug.Log("Object in array: " + objectsFound.Count);
                exploreCountObject("object2", objectsFound);
                Debug.Log("Object in array after: " + objectsFound.Count);
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

    private void TriggerOneLineDialogue(string line)
    {
        // Run the dialogue
        YarnFunctions.storage.SetValue("$interactMsg", line);
        dialogueRunner.StartDialogue("InteractObject");
    }

    private void exploreCountObject(string objectID, List<string> objectsFound)
    {
        if (!objectsFound.Contains(objectID)) // Check if objectID is unique
        {
            objectsFound.Add(objectID); // Add only if it's unique
        }

        if (objectsFound.Count == 3)
        {
            GameManager.setGameState(15);
            OnDialogueStart();
            dialogueRunner.StartDialogue("feedBillyNode");
        }
    }
}
