using System.Collections.Generic;
using TMPro;

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HotBarcodeManager : MonoBehaviour
{
    public static HotBarcodeManager Instance { get; private set; }

    int largestFound = 0;
    int activePlayer = 0;

    [SerializeField] private TextMeshProUGUI instructionText, actionText;
    [SerializeField] private AudioClip success, failure, win, loss;

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

    public void OnBarcodeFound(string text)
    {
        try
        {
            int barcodeValue = int.Parse(text);
            //if(barcodeVal)
            
        }catch(System.Exception e)
        {
            StartCoroutine(FlashMessage(actionText, Color.red, "Invalid barcode!", failure));
            return;
        }
        activePlayer = (activePlayer + 1) % GameState.numPlayers;
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
