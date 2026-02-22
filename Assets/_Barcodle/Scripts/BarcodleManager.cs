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
    bool isPlayerGuessing = true;

    public int[] PlayerAttempts => playerAttempts;
    public bool IsPlayerGuessing => isPlayerGuessing;

    private void Awake()
    {
        do
            randomOffset = Random.Range(1 - GameState.PlayerCount, GameState.PlayerCount - 1);
        while (randomOffset == 0);

        playerAttempts = new int[GameState.PlayerCount];

        playersPool = new(GameState.PlayerCount);
        for (int i = 0; i < GameState.PlayerCount; i++)
            playersPool.Add(i);
    }

    private void Start()
    {
        RollPlayers();
    }

    public void DisplayStatus(string text, Color color, int attemptCount)
    {
        if (!IsPlayerGuessing)
            return;

        isPlayerGuessing = false;

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
        isPlayerGuessing = true;
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
            choosingPlayer += GameState.PlayerCount;
        if (choosingPlayer > GameState.PlayerCount - 1)
            choosingPlayer -= GameState.PlayerCount;

        UpdateInstructionText(choosingPlayer);
    }

    private void UpdateInstructionText(int choosingPlayer)
    {
        string choosing = GameState.Players[choosingPlayer + 1];
        string guessing = GameState.Players[currentGuessingPlayer + 1];
        instructionText.text = $"{choosing}, choose a barcode for {guessing} to guess by scanning it.";
    }
}
