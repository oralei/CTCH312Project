using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Yarn.Unity;

public class SeekManager : MonoBehaviour
{
    public interactableObject interactableObject;
    public GameObject NPCBilly;
    public NavMeshAgent agent;

    public DialogueRunner dialogueRunner;
    public blackFadeScreen blackFadeScreen;

    public Animator animator;

    public bool doneCounting;

    [System.Serializable]
    public struct HidingSpot
    {
        public string spotName;
        public Vector3 Location;
        public float Rotation;
        public bool Crouching;
        public bool Laying;
    }

    [SerializeField]
    public List<HidingSpot> hidingSpots = new List<HidingSpot>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueRunner.onDialogueComplete.AddListener(OnDialogueEnd);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Changes game state depending on round of hide and seek
    // calls hideBilly function
    private void OnTriggerEnter(Collider other)
    {
        blackFadeScreen.FadeToBlack(blackFadeScreen.fadeDuration);
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.gameEventState == 50)
            {
                GameManager.setGameState(55);
                doneCounting = true;
            }
            else if(GameManager.Instance.gameEventState == 56)
            {
                GameManager.setGameState(60);
                doneCounting = true;
            }
            else if (GameManager.Instance.gameEventState == 61)
            {
                GameManager.setGameState(65);
                doneCounting = true;
            }
            else if (GameManager.Instance.gameEventState == 66)
            {
                GameManager.setGameState(70);
                doneCounting = true;
            }
            if (GameManager.Instance.gameEventState >= 50 && GameManager.Instance.gameEventState <= 66)
            {
                interactableObject.OnDialogueStart();
                interactableObject.dialogueRunner.StartDialogue("hideAndSeekNode");
                hideBilly(hidingSpots[Random.Range(0, hidingSpots.Count)]);
            }
            else
            {
                HidingSpot topStairs = new HidingSpot
                {
                    Location = new Vector3(-7.03700018f, 1.26699996f, -1.75399995f),
                    Rotation = -180f,
                    spotName = "Top of stairs"

                };
                interactableObject.OnDialogueStart();
                interactableObject.dialogueRunner.StartDialogue("hideAndSeekNode");
                hideBilly(topStairs);
            }
        }
    }

    // Places Billy at hiding spot depending on struct
    public void hideBilly(HidingSpot hs)
    {
        Debug.Log("Hiding at " + hs.spotName);
        agent.Warp(hs.Location);
        NPCBilly.transform.eulerAngles = new Vector3(0, hs.Rotation, 0);
        hidingSpots.Remove(hs);

        if(hs.Crouching)
        {
            animator.SetBool("isCrouching", true);
        }
        if (hs.Laying)
        {
            animator.SetBool("isLaying", true);
        }
    }

    private void OnDialogueEnd()
    {
        if (doneCounting)
            blackFadeScreen.FadeFromBlack(1f);
    }
}
