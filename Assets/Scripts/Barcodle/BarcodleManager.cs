using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarcodleManager : MonoBehaviour
{
    [SerializeField] GameObject startingScreen;
    [SerializeField] GameObject[] disableOnStart;
    [SerializeField] GameObject statusText;
    [SerializeField] float newRoundDelay;
    [SerializeField] InputAttempt inputAttempt;
    [SerializeField] GameObject placements;
    [SerializeField] int[] playerAttempts;
    int currentPlayer = 0;

    public int[] PlayerAttempts => playerAttempts;

    private void Awake()
    {
        playerAttempts = new int[GameState.numPlayers];
    }

    public void DisplayStatus(string text, Color color, int attemptCount)
    {
        playerAttempts[currentPlayer] = attemptCount;

        statusText.GetComponentInChildren<TMP_Text>().text = text;
        statusText.GetComponent<Image>().color = color;

        statusText.SetActive(true);

        Invoke(nameof(NewRound), newRoundDelay);
    }

    void NewRound()
    {
        currentPlayer++;
        inputAttempt.NewRound();

        foreach (GameObject obj in disableOnStart)
            obj.SetActive(false);

        if (currentPlayer >= playerAttempts.Length)
        {
            placements.SetActive(true);
            return;
        }

        startingScreen.SetActive(true);
    }
}
