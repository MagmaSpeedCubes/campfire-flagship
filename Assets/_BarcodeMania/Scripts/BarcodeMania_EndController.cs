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

        WinnerText.text = $"Player {GetWinningPlayer()} wins with a score of {BarcodeMania_GameData.Instance.PlayerScores[GetWinningPlayer() - 1]}!";
    }

    public void ContinueToMenu(string _)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(MenuSceneName);
        BarcodeMania_GameData.Instance.PlayerScores.Clear();
    }

    public int GetWinningPlayer()
    {
        List<float> scores = BarcodeMania_GameData.Instance.PlayerScores;

        if (scores == null || scores.Count == 0)
        {
            Debug.LogWarning("No player scores available.");
            return -1;
        }

        int highestIndex = 0;
        float highestScore = scores[0];

        for (int i = 1; i < scores.Count; i++)
        {
            if (scores[i] > highestScore)
            {
                highestScore = scores[i];
                highestIndex = i;
            }
        }

        return highestIndex + 1;
    }
}
