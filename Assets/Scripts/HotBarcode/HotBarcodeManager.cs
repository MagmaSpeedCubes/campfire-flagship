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
    [SerializeField] private UIBar timerBar;

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
        timerBar.SetBarValue(Mathf.Ceil(timer));
        if(timer <= 0f)
        {

            StartCoroutine(FlashMessage(alertText, Color.red, $"Player {activePlayer + 1} ran out of time!", loss));
            playersInGame.Remove(activePlayer);
            activePlayer = (activePlayer + 1) % GameState.numPlayers;
            

            if(playersInGame.Count == 1)
            {
                
                EndGame(playersInGame[0] + 1);
            }
            else
            {
                objectiveText.text = $"Player {activePlayer + 1} objective: Find a barcode";
                timer = roundTime;
            }   
        }
    }

    public void EndGame(int winner)
    {
        gameActive = false;
        objectiveText.text = "Game Over! Player " + (winner) + " wins with a score of " + largestFound + "";
        alertText.text = "";
        playersInGame.Clear();
        GetComponent<AudioSource>().Stop();
    }

    public void OnBarcodeFound(string text)
    {
        if(!gameActive) return;
        Debug.Log(text);
        try
        {
            long barcodeValue = long.Parse(text);
            int barcodeScore = sumDigits(barcodeValue);

            if(barcodeScore > largestFound)
            {
                StartCoroutine(FlashMessage(alertText, Color.green, $"Player {activePlayer + 1} found a new largest barcode score: {barcodeValue} has a score of {barcodeScore}", success));

                largestFound = barcodeScore;
                activePlayer = (activePlayer + 1) % GameState.numPlayers;
                timer = roundTime;
                objectiveText.text = $"Player {activePlayer + 1} objective: Find a barcode with a score higher than {largestFound}";


            }
            else
            {
                StartCoroutine(FlashMessage(alertText, Color.yellow, $"{barcodeValue}'s score of {barcodeScore} is not larger than {largestFound}. Try again!", failure));
                timer -= 5f; // Penalty for low score
            }
            
        }catch(System.Exception e)
        {
            StartCoroutine(FlashMessage(alertText, Color.red, $"{text} is not a valid barcode!", failure));
            timer -= 5f; // Penalty for invalid barcode
            return;
        }
        
    }

    IEnumerator FlashMessage(TextMeshProUGUI text, Color color, string content, AudioClip clip)
    {
        text.text = content;
        text.color = color;
        GetComponent<AudioSource>().PlayOneShot(clip);
        //yield return new WaitForSeconds(2f);
        //text.text = "";
        yield return null;
    }

    int sumDigits(long number)
    {
        int sum = 0;
        while (number > 0)
        {
            sum += (int)(number % 10);
            number /= 10;
        }
        return sum;
    }

    


}

