using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetName : MonoBehaviour
{
    public TMP_InputField input;
    [SerializeField] private string mainScene = "MainScene";
    public void PlayerName()
    {
        FPSController.playerName = input.text;
        SceneManager.LoadScene(mainScene);
        Debug.Log(name);
    }
    
}
