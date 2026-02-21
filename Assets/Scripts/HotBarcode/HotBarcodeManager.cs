using System.Collections.Generic;
using System.Collections;
using TMPro;

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HotBarcodeManager : MonoBehaviour
{
    public static HotBarcodeManager Instance { get; private set; }

    int largestFound = 0;
    int activePlayer = 0;
    List<int> playersInGame = new List<int>();
    float timer = 0f;
    [SerializeField] private float roundTime = 60f;
    bool gameActive = false;

    [SerializeField] private TextMeshProUGUI objectiveText, alertText, timerText;
    [SerializeField] private AudioClip success, failure, win, loss;
    [SerializeField] private AudioClip backgroundMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StartGame()
    {
        gameActive = true;
        largestFound = 0;
        activePlayer = 0;
        timer = roundTime;
        objectiveText.text = $"Player {activePlayer + 1} objective: Find a barcode";
        alertText.text = "";

        for (int i = 0; i < GameState.numPlayers; i++)
        {
            playersInGame.Add(i);
        }
        AudioSource asc = GetComponent<AudioSource>();
        asc.clip = backgroundMusic;
        asc.loop = true;
        asc.Play();

    }

    void Update()
    {
        if (!gameActive) return;


        timer -= Time.deltaTime;
        timerText.text = $"Time: {Mathf.Ceil(timer)}s";
        if(timer <= 0f)
        {

            StartCoroutine(FlashMessage(alertText, Color.red, $"Player {activePlayer + 1} ran out of time!", loss));
            activePlayer = (activePlayer + 1) % GameState.numPlayers;
            playersInGame.Remove(activePlayer);

            if(playersInGame.Count == 1)
            {
                StartCoroutine(FlashMessage(alertText, Color.green, $"Player {playersInGame[0] + 1} wins!", win));
                EndGame();
            }
            else
            {
                objectiveText.text = $"Player {activePlayer + 1} objective: Find a barcode";
                timer = roundTime;
            }   
        }
    }

    public void EndGame()
    {
        gameActive = false;
        objectiveText.text = "Game Over!";
        alertText.text = "";
        playersInGame.Clear();
    }

    public void OnBarcodeFound(string text)
    {
        try
        {
            int barcodeValue = int.Parse(text);
            if(barcodeValue > largestFound)
            {
                StartCoroutine(FlashMessage(alertText, Color.green, $"Player {activePlayer + 1} found a new largest barcode: {barcodeValue}!", success));

                largestFound = barcodeValue;
                objectiveText.text = $"Player {activePlayer + 1} objective: Find a larger barcode than {largestFound}";
                activePlayer = (activePlayer + 1) % GameState.numPlayers;
                timer = roundTime;
            }
            else
            {
                StartCoroutine(FlashMessage(alertText, Color.yellow, $"{barcodeValue} is not larger than {largestFound}. Try again!", failure));
            }
            
        }catch(System.Exception e)
        {
            StartCoroutine(FlashMessage(alertText, Color.red, "Invalid barcode!", failure));
            return;
        }
        
    }

    IEnumerator FlashMessage(TextMeshProUGUI text, Color color, string content, AudioClip clip)
    {
        text.text = content;
        text.color = color;
        GetComponent<AudioSource>().PlayOneShot(clip);
        yield return new WaitForSeconds(2f);
        text.text = "";
    }

    


}
