using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int gameEventState = 0;

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
    }
}
