using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Yarn.Unity;

[RequireComponent(typeof(CharacterController))]

public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public GetName getName;

    // Yarn Variables
    public static string playerName;
    public static int playerAge = 21;

    // Movement Variables
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public bool canMove = true;
    public bool canMoveMouse = true;

    // Interaction Variables
    public float interactDistance = 1.5f;
    public LayerMask interactableLayer; // Assign a layer for interactable objects
    private IInteractable currentInteractable = null; // Store the currently looked-at interactable object
    private Outline currentOutline = null; // Store the outline component
    public TextMeshProUGUI interactText; // enable and disable on hover

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    public DialogueRunner dialogueRunner;

    public BillyMovement BillyMovement;

    public characterVoice characterVoice;

    //public KeyCode continueActionKeyCode = KeyCode.Space;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    // Items

    CharacterController characterController;
    
    // Sets default name to Bob if player did not select a name to use
    private void Awake()
    {
        if(playerName == null || playerName == "")
        {
            playerName = "Bob";
        }
    }
    
    // Subscribes to onDialogueEnd event and diables cursor 
    void Start()
    {
        dialogueRunner.onDialogueComplete.AddListener(OnDialogueEnd);

        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    // Constantly checks player input for object interaction and movement
    void Update()
    {
        CheckForInteractable();

        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log(GameManager.Instance.gameEventState);
        }

        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMoveMouse)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion
    }

    // Uses raycast to check for objects on the interactable layer
    void CheckForInteractable()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            // If the hit object is on the interactable layer
            if (((1 << hit.collider.gameObject.layer) & interactableLayer) != 0)
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                Outline outline = hit.collider.GetComponent<Outline>();

                if (interactable != null)
                {
                    if (currentInteractable != interactable) // Only update if new object is detected
                    {
                        ClearOutline(); // Remove outline from previous object
                        currentInteractable = interactable;
                        currentOutline = outline;
                        if (currentOutline != null)
                        {
                            currentOutline.enabled = true; // Enable outline
                        }
                    }

                    interactText.gameObject.SetActive(true); // Show text
                    string objectTag = hit.collider.tag;
                    switch (objectTag)
                    {
                        case "NPC":
                            interactText.text = "[E] Talk";
                            break;

                        case "Food":
                            interactText.text = "[E] Eat";
                            break;

                        case "Item":
                            interactText.text = "[E] Pick Up";
                            break;

                        case "Dirty":
                            interactText.text = "[E] Clean Up";
                            break;

                        case "Cat":
                            interactText.text = "[E] Pet";
                            break;

                        default:
                            interactText.text = "[E] Interact";
                            break;
                    }

                    return;
                }
            }
        }

        interactText.gameObject.SetActive(false); // Hide text when not hovering
        ClearOutline(); // No object detected, clear outline
    }

    // Deactivates outline script
    void ClearOutline()
    {
        if (currentOutline != null)
        {
            currentOutline.enabled = false; // Disable outline
        }
        currentInteractable = null;
        currentOutline = null;
    }

    // Reactivates player movement when dialogue finishes
    public void OnDialogueEnd()
    {
        Debug.Log("Dialogue has finished!");
        characterVoice.characterName.text = "";

        // Re-enable player movement
        canMove = true;
        canMoveMouse = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (dialogueRunner.VariableStorage.TryGetValue<bool>("$watchingTV", out var result) && GameManager.Instance.gameEventState <= 25)
        {
            if (result)
            {
                BillyMovement.MoveToDestination(new Vector3(0.171000004f, 0.134000003f, -7.98799992f), new Quaternion(0, 1, 0, 0));
            }
        }

        if (GameManager.Instance.gameEventState == 40)
        {
            GameManager.Instance.UpdateTaskText("Clean up the broken vase");
        }
    }
}

// Interface for interactable objects
public interface IInteractable
{
    void Interact();
}

//Vector3(0.171000004, 0.134000003, -8.16499996)