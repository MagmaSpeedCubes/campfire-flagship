using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BarcodeMania_EndController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private string MenuSceneName;
    [SerializeField] private Text WinnerText;

    public void Start()
    {
        ReadBarcode.Instance.OnBarcodeScanned.AddListener(ContinueToMenu);

        WinnerText.text = $"Player {GameState.Players[GetWinningPlayer()]} wins with a score of {BarcodeMania_GameData.Instance.PlayerScores[GetWinningPlayer()]}!";
    }

    public void ContinueToMenu(string _)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(MenuSceneName);
        BarcodeMania_GameData.Instance.PlayerScores.Clear();
    }

    public int GetWinningPlayer()
    {
        int highestScore = -1;
        foreach(int score in BarcodeMania_GameData.Instance.PlayerScores)
        {
            if (score > highestScore) highestScore = score;
        }
        return BarcodeMania_GameData.Instance.PlayerScores.IndexOf(highestScore);
    }
}
