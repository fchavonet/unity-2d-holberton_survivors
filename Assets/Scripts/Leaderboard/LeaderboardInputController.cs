using TMPro;
using UnityEngine;

public class LeaderboardInputController : MonoBehaviour
{
    // Reference to the TMP_InputField for player name input
    public TMP_InputField inputField;

    // Maximum number of characters allowed in the input field
    public int maxCharacters = 10;

    void Start()
    {
        // Set the character limit of the input field
        inputField.characterLimit = maxCharacters;

        // Add a listener to detect changes in the input field text and remove spaces
        inputField.onValueChanged.AddListener(delegate { RemoveSpaces(); });
    }

    // Remove spaces from the input field text
    void RemoveSpaces()
    {
        // Replace any spaces in the input field text with an empty string
        inputField.text = inputField.text.Replace(" ", "");
    }
}
