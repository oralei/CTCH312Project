using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class prefixedInput : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerName2;
    private string prefix = "<color=#FFE75F>My name is</color> ";

    void Start()
    {
        // Ensure input starts correctly
        inputField.text = prefix;
        inputField.onValueChanged.AddListener(EnforcePrefix);
        inputField.onSelect.AddListener(PlaceCursorAtEnd);
    }

    private void Update()
    {
        //playerName.text = GetUserInput();
        //playerName2.text = inputField.text;
    }

    void EnforcePrefix(string text)
    {
        // Prevent deletion of the prefix
        if (!text.StartsWith(prefix))
        {
            inputField.text = prefix;
            inputField.caretPosition = prefix.Length;
        }
    }

    void PlaceCursorAtEnd(string _)
    {
        // Move cursor to end of the static text
        inputField.caretPosition = prefix.Length;
    }

    public string GetUserInput()
    {
        // Remove the prefix from the text and return the user input
        if (inputField.text.StartsWith(prefix))
        {
            return inputField.text.Substring(prefix.Length); // Remove the prefix
        }
        return inputField.text; // In case there's an issue (shouldn't happen)
    }

}