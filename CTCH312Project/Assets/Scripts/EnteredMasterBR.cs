using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class EnteredMasterBR : MonoBehaviour
{
    public GameObject triggerVolume;

    public DoorController door1;
    public DoorController door2;
    public DoorController door3;
    public DoorController door4;

    bool ready = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Closes doors and activates running ghost jump scare
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ready)
        {
            // close other doors
            if (door1.isOpening == true)
                door1.isOpening = false;

            if (door2.isOpening == true)
                door2.isOpening = false;

            if (door3.isOpening == true)
                door3.isOpening = false;

            if (door4.isOpening == true)
                door4.isOpening = false;

            triggerVolume.SetActive(true);
            Debug.Log("Ghost ready to spook!");
            ready = false;
        }
    }
}
