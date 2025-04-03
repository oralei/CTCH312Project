using UnityEngine;
using UnityEngine.SceneManagement;

public class returnToMenu : MonoBehaviour
{
    private bool canReturn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("toggleReturn", 69.95f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey && canReturn)
        {
            canReturn = false;
            SceneManager.LoadScene("MainMenu");
        }
    }

    void toggleReturn()
    {
        canReturn = true;
    }
}
