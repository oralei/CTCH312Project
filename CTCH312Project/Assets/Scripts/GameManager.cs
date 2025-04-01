using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SeekManager seekManager;
    public int gameEventState = 0;

    public GameObject pizzaArrow;

    public closeAllDoors closeAllDoors;

    public GameObject HS_Handler;
    public GameObject BasementWall;
    public GameObject playerLight;

    public GameObject Lights1;
    public GameObject Lights2;
    public GameObject StairLight;

    public GameObject shadowMan1;
    public GameObject shadowMan2;

    public GameObject darkEyes;
    public GameObject bodyBagTrigger;

    public GameObject finalBarriers;

    public TextMeshProUGUI taskText; // Reference to TMP text component
    public Color blinkColor = Color.yellow; // Blink color
    public float blinkDuration = 0.2f; // Time per blink

    public Volume PostProcessing;
    private FilmGrain filmGrain;
    private ChromaticAberration chromaticAberration;
    private ColorAdjustments colorAdjustments;

    AudioManager audioManager;

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
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        Instance.PostProcessing.profile.TryGet(out filmGrain);
        Instance.PostProcessing.profile.TryGet(out chromaticAberration);
        Instance.PostProcessing.profile.TryGet(out colorAdjustments);
    }

    public static void setGameState(int newState)
    {
        Instance.gameEventState = newState;
        Debug.Log("Set state to " + Instance.gameEventState);

        if (Instance.gameEventState == 15)
        {
            Instance.pizzaArrow.SetActive(true);
        }

        // Round 1
        if (Instance.gameEventState == 50)
        {
            Instance.HS_Handler.SetActive(true);
        }
        else if (Instance.gameEventState == 55)  // -------- Start Seeking
        {
            // Post Processing Changes:
            Instance.filmGrain.intensity.value = 0.8f;

            Instance.closeAllDoors.closeDoors();

            GhostJumpScare.Instance.setupJS();
            GhostJumpScare.Instance.ready = true;

            Instance.darkEyes.SetActive(true);

            Instance.shadowMan1.SetActive(true);

            Instance.HS_Handler.SetActive(false);
        }
        // Round 2
        else if (Instance.gameEventState == 56)
        {
            Instance.HS_Handler.SetActive(true);
            Instance.UpdateTaskText("Go to the corner and count");
        }
        else if (Instance.gameEventState == 60)   // -------- Seeking
        {
            Instance.closeAllDoors.closeDoors();
            // Activate stairs shadowman
            Instance.shadowMan2.SetActive(true);

            PhoneJS.Instance.ready = true;

            Instance.HS_Handler.SetActive(false);
        }
        // Round 3
        else if (Instance.gameEventState == 61)
        {
            Instance.HS_Handler.SetActive(true);
            Instance.UpdateTaskText("Go to the corner and count");
        }
        else if (Instance.gameEventState == 65)   // -------- Seeking
        {
            Instance.closeAllDoors.closeDoors();

            //Instance.colorAdjustments.colorFilter.value = new Color(123f / 255f, 161f / 255f, 125f / 255f, 0);
            Instance.chromaticAberration.intensity.value = 1f;

            Instance.bodyBagTrigger.SetActive(true);  // 1.
            Instance.HS_Handler.SetActive(false);
        }
        // Final Round (Top stairs)
        else if (Instance.gameEventState == 66)
        {
            Instance.HS_Handler.SetActive(true);
            Instance.UpdateTaskText("Go to the corner and count");
        }
        else if (Instance.gameEventState == 70)   // -------- Seeking last (top stairs)
        {

            Instance.closeAllDoors.closeDoors();
            // Disable these
            Instance.HS_Handler.SetActive(false);
            Instance.BasementWall.SetActive(false);
            Instance.Lights1.SetActive(false);
            Instance.Lights2.SetActive(false);

            // Enable these
            Instance.playerLight.SetActive(true);
            Instance.StairLight.SetActive(true);
            Instance.finalBarriers.SetActive(true);
        }

        Instance.playBlinkText();
        Instance.audioManager.PlaySFX(Instance.audioManager.notify);
    }

    public void UpdateTaskText(string newTask)
    {
        taskText.text = "Current Task: " + newTask;
    }

    public void playBlinkText()
    {
        Instance.StartCoroutine(Instance.BlinkText(2)); // Blink twice
    }

    IEnumerator BlinkText(int times)
    {
        Color originalColor = Color.white;

        for (int i = 0; i < times; i++)
        {
            taskText.color = blinkColor;
            yield return new WaitForSeconds(blinkDuration);
            taskText.color = originalColor;
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}
