using UnityEngine;
using Yarn.Unity;

public class YarnFunctions : MonoBehaviour
{
    [SerializeField] public InMemoryVariableStorage storage;   

    [YarnCommand] public int GetState()
    {
        storage.SetValue("$gameState", GameManager.Instance.gameEventState);

        return GameManager.Instance.gameEventState;
    }

    [YarnCommand("setState")] public static void SetState(int newNumber) 
    { 
        GameManager.setGameState(newNumber);
    }

    [YarnCommand] public void GetName()
    {
        storage.SetValue("$playerName", FPSController.playerName);
    }

    [YarnCommand] public void GetAge()
    {
        storage.SetValue("$playerAge", FPSController.playerAge);
    }

    [YarnCommand] public void parentsLeave()
    {
        Destroy(GameObject.Find("NPC_Mom"));
    }
}
