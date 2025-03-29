using System.Collections.Generic;
using UnityEngine;

public class SeekManager : MonoBehaviour
{
    public interactableObject interactableObject;
    public GameObject NPCBilly;

    [System.Serializable]
    public struct HidingSpot
    {
        public Vector3 Location;
        public Quaternion Rotation;
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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactableObject.OnDialogueStart();
            interactableObject.TriggerOneLineDialogue("Let the games begin...");
        }
    }
}
