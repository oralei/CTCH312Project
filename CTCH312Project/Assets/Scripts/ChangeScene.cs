using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    public float changeTime;
    public string sceneName;

    private void Update()
    {
        SceneManager.LoadScene(sceneName);
        if(changeTime <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
