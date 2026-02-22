using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayersSetupManager : MonoBehaviour
{
    [SerializeField] TMP_InputField amountInputField;
    [SerializeField] Button confirmAmountButton;

    [SerializeField] TMP_Text playerNameInstruction;
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] Button confirmNameButton;
    string[] players;
    int playerInputIndex = 0;

    public void ValidateAmountInput(string input)
    {
        confirmAmountButton.interactable = !string.IsNullOrWhiteSpace(input);
    }

    public void ConfirmAmount()
    {
        players = new string[int.Parse(amountInputField.text)];
        UpdatePlayerNameInstructionText();
    }

    public void ValidatePlayerName(string input)
    {
        confirmNameButton.interactable = !string.IsNullOrWhiteSpace(input);
    }

    public void ConfirmPlayerName()
    {
        players[playerInputIndex] = playerNameInputField.text;
        playerInputIndex++;

        if (playerInputIndex >= players.Length)
        {
            GameState.Initialize(players);
            SceneManager.LoadScene(1);
            return;
        }

        playerNameInputField.text = string.Empty;

        UpdatePlayerNameInstructionText();
    }

    void UpdatePlayerNameInstructionText()
    {
        playerNameInstruction.text = $"Enter player {playerInputIndex + 1} name.";
    }
}
