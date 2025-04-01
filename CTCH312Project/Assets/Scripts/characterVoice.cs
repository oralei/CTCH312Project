using TMPro;
using UnityEngine;
using Yarn.Unity;

public class characterVoice : MonoBehaviour
{
    public AudioSource audioSource; // Assign an AudioSource in the Inspector
    public AudioClip typeSound; // Assign a typing sound clip

    public LineView lineView;
    public DialogueRunner runner;

    public TextMeshProUGUI dialogueText; // Reference to the text component
    public TextMeshProUGUI characterName; // Reference to the text component

    private string fullText = ""; // Stores the full dialogue text
    private int revealedCharacterCount = 0; // Tracks how many characters have been revealed

    AudioManager audioManager;

    public AudioClip a;
    public AudioClip e;
    public AudioClip i;
    public AudioClip o;
    public AudioClip u;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        if (lineView != null)
        {
            runner.onNodeStart.AddListener(resetCounter);
            lineView.onCharacterTyped.AddListener(OnCharacterTyped);
        }
    }

    private void resetCounter(string test)
    {
        Debug.Log(test);
        revealedCharacterCount = 0;
    }

    private void OnCharacterTyped()
    {
        if (dialogueText != null)
        {
            // Get the next revealed character
            if (dialogueText.text != fullText)
            {
                fullText = dialogueText.text;
                revealedCharacterCount = 0;
            }

            char typedCharacter = fullText[revealedCharacterCount];
            revealedCharacterCount++;

            Debug.Log("Character typed: " + typedCharacter);

            switch (typedCharacter) {
                case 'a':
                    audioManager.PlaySFX(a);
                    break;
                case 'e':
                    audioManager.PlaySFX(e);
                    break;
                case 'i':
                    audioManager.PlaySFX(i);
                    break;
                case 'o':
                    audioManager.PlaySFX(o);
                    break;
                case 'u':
                    audioManager.PlaySFX(u);
                    break;

                default:
                break;
            }
            //audioManager.PlaySFX(audioManager.notify);
        }
    }
}