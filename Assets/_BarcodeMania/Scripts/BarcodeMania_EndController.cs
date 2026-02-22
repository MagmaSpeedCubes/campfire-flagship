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

        int winnerIndex = GetWinningPlayer();
        if (winnerIndex == -1)
        {
            WinnerText.text = "No winner: no scores available.";
        }
        else
        {
            WinnerText.text = $"Player {GameState.Players[winnerIndex]} wins with a score of {BarcodeMania_GameData.Instance.PlayerScores[winnerIndex]}!";
        }
    }

    public void ContinueToMenu(string _)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(MenuSceneName);
        BarcodeMania_GameData.Instance.PlayerScores.Clear();
    }

    public int GetWinningPlayer()
    {
        var scores = BarcodeMania_GameData.Instance.PlayerScores;
        var times = BarcodeMania_GameData.Instance.PlayerTimes;
        if (scores == null || scores.Count == 0) return -1;

        float highestScore = float.MinValue;
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i] > highestScore) highestScore = scores[i];
        }

        List<int> tiedIndices = new List<int>();
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i] == highestScore) tiedIndices.Add(i);
        }

        if (tiedIndices.Count == 1)
        {
            return tiedIndices[0];
        }

        float highestTime = float.MinValue;
        int winnerIndex = tiedIndices[0];
        foreach (int idx in tiedIndices)
        {
            if (times != null && idx < times.Count && times[idx] > highestTime)
            {
                highestTime = times[idx];
                winnerIndex = idx;
            }
        }
        return winnerIndex;
    }
}
