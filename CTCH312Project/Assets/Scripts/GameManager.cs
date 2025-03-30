using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int gameEventState = 0;

    public GameObject HS_Handler;
    public GameObject BasementWall;
    public GameObject playerLight;

    public GameObject Lights1;
    public GameObject Lights2;
    public GameObject StairLight;


    public TextMeshProUGUI taskText; // Reference to your TMP text component

    public HashSet<string> objectsFound = new HashSet<string>(); // HashSet prevents duplicates automatically

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void setGameState(int newState)
    {
        Instance.gameEventState = newState;
        Debug.Log("Set state to " + Instance.gameEventState);

        // Round 1
        if (Instance.gameEventState == 50)
        {
            Instance.HS_Handler.SetActive(true);
        }
        else if (Instance.gameEventState == 55)
        {
            Instance.HS_Handler.SetActive(false);
        }
        // Round 2
        else if (Instance.gameEventState == 56)
        {
            Instance.HS_Handler.SetActive(true);
            Instance.UpdateTaskText("Go to the corner and count");
        }
        else if (Instance.gameEventState == 60)
        {
            Instance.HS_Handler.SetActive(false);
        }
        // Round 3
        else if (Instance.gameEventState == 61)
        {
            Instance.HS_Handler.SetActive(true);
            Instance.UpdateTaskText("Go to the corner and count");
        }
        else if (Instance.gameEventState == 65)
        {
            Instance.HS_Handler.SetActive(false);
        }
        // Final Round (Top stairs)
        else if (Instance.gameEventState == 66)
        {
            Instance.HS_Handler.SetActive(true);
            Instance.UpdateTaskText("Go to the corner and count");
        }
        // Seeking last
        else if (Instance.gameEventState == 70)
        {
            // Disable these
            Instance.HS_Handler.SetActive(false);
            Instance.BasementWall.SetActive(false);
            Instance.Lights1.SetActive(false);
            Instance.Lights2.SetActive(false);

            // Enable these
            Instance.playerLight.SetActive(true);
            Instance.StairLight.SetActive(true);

        }
    }

    public void UpdateTaskText(string newTask)
    {
        taskText.text = "Current Task: " + newTask;
    }
}
