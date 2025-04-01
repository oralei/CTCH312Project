using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
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

    [Header("---------Billy Voice Files---------")]
    public List<AudioClip> BillyClips = new List<AudioClip>();

    [Header("---------Kevin Voice Files---------")]
    public List<AudioClip> KevinClips = new List<AudioClip>();

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

            if (characterName.text == "Billy")
            {
                switch (typedCharacter)
                {
                    case 'a':
                        audioManager.PlaySFX(BillyClips[0]);
                        break;
                    case 'e':
                        audioManager.PlaySFX(BillyClips[1]);
                        break;
                    case 'i':
                        audioManager.PlaySFX(BillyClips[2]);
                        break;
                    case 'o':
                        audioManager.PlaySFX(BillyClips[3]);
                        break;
                    case 'u':
                        audioManager.PlaySFX(BillyClips[4]);
                        break;

                    default:
                        break;
                }
            }
            if (characterName.text == "Kevin")
            {
                switch (typedCharacter)
                {
                    case 'a':
                        audioManager.PlaySFX(KevinClips[0]);
                        break;
                    case 'e':
                        audioManager.PlaySFX(KevinClips[1]);
                        break;
                    case 'i':
                        audioManager.PlaySFX(KevinClips[2]);
                        break;
                    case 'o':
                        audioManager.PlaySFX(KevinClips[3]);
                        break;
                    case 'u':
                        audioManager.PlaySFX(KevinClips[4]);
                        break;

                    default:
                        break;
                }
            }
            //audioManager.PlaySFX(audioManager.notify);
        }
    }
}