using System.Collections.Generic;
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
    [SerializeField] TMP_Text instructionText;
    [SerializeField] GameObject placements;
    [SerializeField] int[] playerAttempts;
    int currentGuessingPlayer = 0;
    List<int> guessingPlayersPool;
    List<int> choosingPlayersPool;

    public int[] PlayerAttempts => playerAttempts;

    private void Awake()
    {
        playerAttempts = new int[GameState.numPlayers];

        guessingPlayersPool = new(GameState.numPlayers);
        choosingPlayersPool = new(GameState.numPlayers);
        for (int i = 0; i < GameState.numPlayers; i++)
        {
            guessingPlayersPool.Add(i);
            choosingPlayersPool.Add(i);
        }
    }

    private void Start()
    {
        RollPlayers();
    }

    public void DisplayStatus(string text, Color color, int attemptCount)
    {
        playerAttempts[currentGuessingPlayer] = attemptCount;

        statusText.GetComponentInChildren<TMP_Text>().text = text;
        statusText.GetComponent<Image>().color = color;

        statusText.SetActive(true);

        Invoke(nameof(NewRound), newRoundDelay);
    }

    void NewRound()
    {
        inputAttempt.NewRound();

        foreach (GameObject obj in disableOnStart)
            obj.SetActive(false);

        if (guessingPlayersPool.Count == 0 || choosingPlayersPool.Count == 0)
        {
            placements.SetActive(true);
            return;
        }

        RollPlayers();
        startingScreen.SetActive(true);
    }

    void RollPlayers()
    {
        currentGuessingPlayer = guessingPlayersPool[Random.Range(0, guessingPlayersPool.Count)];
        guessingPlayersPool.Remove(currentGuessingPlayer);

        List<int> currentChoosingPlayersPool = new(choosingPlayersPool);
        currentChoosingPlayersPool.Remove(currentGuessingPlayer);

        int choosingPlayer = currentChoosingPlayersPool[Random.Range(0, currentChoosingPlayersPool.Count)];
        choosingPlayersPool.Remove(choosingPlayer);


        UpdateInstructionText(choosingPlayer);
    }

    private void UpdateInstructionText(int choosingPlayer)
    {
        instructionText.text = $"Player {choosingPlayer + 1}, choose a barcode for Player {currentGuessingPlayer + 1} to guess by scanning it.";
    }
}
