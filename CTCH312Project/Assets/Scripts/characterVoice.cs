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
    private string voiceName;

    AudioManager audioManager;

    [Header("---------Billy Voice Files---------")]
    public List<AudioClip> BillyClips = new List<AudioClip>();

    [Header("---------Kevin Voice Files---------")]
    public List<AudioClip> KevinClips = new List<AudioClip>();

    [Header("---------Mom Voice Files---------")]
    public List<AudioClip> MomClips = new List<AudioClip>();

    [Header("---------Player Voice Files---------")]
    public List<AudioClip> PlayerClips = new List<AudioClip>();

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
        voiceName = characterName.text;
        revealedCharacterCount = 0;
    }

    private void OnCharacterTyped()
    {
        voiceName = characterName.text;
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

            if (voiceName == "Billy")
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
            else if (voiceName == "Kevin")
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
            else if (voiceName == "Billy's Mom")
            {
                switch (typedCharacter)
                {
                    case 'a':
                        audioManager.PlaySFX(MomClips[0]);
                        break;
                    case 'e':
                        audioManager.PlaySFX(MomClips[1]);
                        break;
                    case 'i':
                        audioManager.PlaySFX(MomClips[2]);
                        break;
                    case 'o':
                        audioManager.PlaySFX(MomClips[3]);
                        break;
                    case 'u':
                        audioManager.PlaySFX(MomClips[4]);
                        break;

                    default:
                        break;
                }
            }
            else
            {
                switch (typedCharacter)
                {
                    case 'a':
                        audioManager.PlaySFX(PlayerClips[0]);
                        break;
                    case 'e':
                        audioManager.PlaySFX(PlayerClips[1]);
                        break;
                    case 'i':
                        audioManager.PlaySFX(PlayerClips[2]);
                        break;
                    case 'o':
                        audioManager.PlaySFX(PlayerClips[3]);
                        break;
                    case 'u':
                        audioManager.PlaySFX(PlayerClips[4]);
                        break;
                    case '1':
                        audioManager.PlaySFX(PlayerClips[5]);
                        break;
                    case '2':
                        audioManager.PlaySFX(PlayerClips[6]);
                        break;
                    case '3':
                        audioManager.PlaySFX(PlayerClips[7]);
                        break;
                    case '4':
                        audioManager.PlaySFX(PlayerClips[8]);
                        break;
                    case '5':
                        audioManager.PlaySFX(PlayerClips[9]);
                        break;
                    case '6':
                        audioManager.PlaySFX(PlayerClips[10]);
                        break;
                    case '7':
                        audioManager.PlaySFX(PlayerClips[11]);
                        break;
                    case '8':
                        audioManager.PlaySFX(PlayerClips[12]);
                        break;
                    case '9':
                        audioManager.PlaySFX(PlayerClips[13]);
                        break;
                    case '0':
                        audioManager.PlaySFX(PlayerClips[14]);
                        break;

                    default:
                        break;
                }
            }
        }
    }
}