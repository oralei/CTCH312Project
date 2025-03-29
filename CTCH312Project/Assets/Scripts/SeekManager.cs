using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class SeekManager : MonoBehaviour
{
    public interactableObject interactableObject;
    public GameObject NPCBilly;
    public NavMeshAgent agent;

    [System.Serializable]
    public struct HidingSpot
    {
        public string spotName;
        public Vector3 Location;
        public float Rotation;
        public Animation Pose;
    }

    [SerializeField]
    public List<HidingSpot> hidingSpots = new List<HidingSpot>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var spot in hidingSpots)
        {
            Debug.Log($"Location: {spot.Location}, Rotation: {spot.Rotation}, Pose: {spot.Pose}");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            hideBilly(hidingSpots[0]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            hideBilly(hidingSpots[1]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            hideBilly(hidingSpots[2]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            hideBilly(hidingSpots[3]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            hideBilly(hidingSpots[4]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            hideBilly(hidingSpots[5]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            hideBilly(hidingSpots[6]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            hideBilly(hidingSpots[7]);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            hideBilly(hidingSpots[8]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.gameEventState == 50)
            {
                GameManager.setGameState(55);
            }
            else if(GameManager.Instance.gameEventState == 56)
            {
                GameManager.setGameState(60);
            }
            else if (GameManager.Instance.gameEventState == 61)
            {
                GameManager.setGameState(65);
            }
            else if (GameManager.Instance.gameEventState == 66)
            {
                GameManager.setGameState(70);
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

    public void hideBilly(HidingSpot hs)
    {
        Debug.Log("Hiding at " + hs.spotName);
        agent.Warp(hs.Location);
        NPCBilly.transform.eulerAngles = new Vector3(0, hs.Rotation, 0);
        hidingSpots.Remove(hs);
    }
}
