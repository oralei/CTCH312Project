using UnityEngine;

public class stateChanger : MonoBehaviour
{
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
        // Check if the collided object has the "Player" tag
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.gameEventState == 0)
            {
                GameManager.setGameState(GameManager.Instance.gameEventState + 5);
            }
            else
                Debug.Log("Already changed");
        }
    }
}
