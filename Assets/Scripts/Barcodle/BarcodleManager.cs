using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarcodleManager : MonoBehaviour
{
    [SerializeField] GameObject startingScreen;
    [SerializeField] GameObject[] disableOnStart;
    [SerializeField] GameObject statusText;
    [SerializeField] float newRoundDelay;
    int playerIndex = 0;
    int[] playerAttempts;

    private void Awake()
    {
        playerAttempts = new int[GameState.numPlayers];
    }

    private void Start()
    {
        NewRound();
    }

    public void DisplayStatus(string text, Color color, int attemptCount)
    {
        playerAttempts[playerIndex] = attemptCount;

        statusText.GetComponentInChildren<TMP_Text>().text = text;
        statusText.GetComponent<Image>().color = color;

        statusText.SetActive(true);

        Invoke(nameof(NewRound), newRoundDelay);
    }

    void NewRound()
    {
        playerIndex++;

        foreach (GameObject obj in disableOnStart)
            obj.SetActive(false);

        startingScreen.SetActive(true);
    }
}
