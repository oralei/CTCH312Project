using UnityEngine;
using static FPSController;

public class interactableObject : MonoBehaviour, IInteractable
{
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
        if (GameManager.Instance.gameEventState == 5){ 
            Debug.Log("Object interacted with!");
            Destroy(gameObject);
        }
        else
            Debug.Log("State not ready!");
    }
}
