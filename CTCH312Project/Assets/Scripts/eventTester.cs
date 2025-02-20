using UnityEngine;

public class eventTester : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameEventState == 5)
        {
            Debug.Log("I am in state 5");
            GameManager.Instance.gameEventState++;
        }
    }
}
