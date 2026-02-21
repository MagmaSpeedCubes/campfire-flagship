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
    List<int> playersPool;
    int randomOffset;

    public int[] PlayerAttempts => playerAttempts;

    private void Awake()
    {
        randomOffset = Random.Range(1 - GameState.numPlayers, GameState.numPlayers - 1);
        playerAttempts = new int[GameState.numPlayers];

        playersPool = new(GameState.numPlayers);
        for (int i = 0; i < GameState.numPlayers; i++)
            playersPool.Add(i);
    }

    private void Start()
    {
        RollPlayers();
    }

    public void DisplayStatus(string text, Color color, int attemptCount)
    {
        const float statusBarAlpha = 0.75f;
        color.a = statusBarAlpha;
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

        if (playersPool.Count == 0)
        {
            placements.SetActive(true);
            return;
        }

        RollPlayers();
        startingScreen.SetActive(true);
    }

    void RollPlayers()
    {
        currentGuessingPlayer = playersPool[Random.Range(0, playersPool.Count)];
        playersPool.Remove(currentGuessingPlayer);

        int choosingPlayer = currentGuessingPlayer + randomOffset;
        if (choosingPlayer < 0)
            choosingPlayer += GameState.numPlayers;
        if (choosingPlayer > GameState.numPlayers - 1)
            choosingPlayer -= GameState.numPlayers;

        UpdateInstructionText(choosingPlayer);
    }

    private void UpdateInstructionText(int choosingPlayer)
    {
        instructionText.text = $"Player {choosingPlayer + 1}, choose a barcode for Player {currentGuessingPlayer + 1} to guess by scanning it.";
    }
}
