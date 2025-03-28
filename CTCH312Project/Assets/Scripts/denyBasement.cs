using UnityEngine;

public class denyBasement : MonoBehaviour
{
    public interactableObject interactableObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
            interactableObject.TriggerOneLineDialogue("I was told I shouldn't go down there...");
        }
    }
}
