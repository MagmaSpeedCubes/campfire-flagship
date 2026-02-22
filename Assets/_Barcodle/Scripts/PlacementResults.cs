using EditorAttributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class PlacementResults : MonoBehaviour
{
    [SerializeField] TMP_Text runnerupPrefab;
    [SerializeField] TMP_Text winnerText;
    [SerializeField] BarcodleManager gameManager;
    [SerializeField] InputAttempt inputAttempt;
    [SerializeField] float exitDelay;
    [SerializeField] GameObject exitInfoText;
    bool canExit = false;

    [Button("Calculate Placements", 36)]
    private void Start()
    {
        for (int i = 2; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);

        winnerText.text = "Winner(s):\n";

        SortedDictionary<int, List<int>> attemptsToPlayer = new();
        for (int i = 0; i < gameManager.PlayerAttempts.Length; i++)
        {
            int attempts = gameManager.PlayerAttempts[i];
            if (!attemptsToPlayer.TryGetValue(attempts, out var playerIndexes))
            {
                playerIndexes = new();
                attemptsToPlayer[attempts] = playerIndexes;
            }
            playerIndexes.Add(i + 1);
        }

        int maxScore = attemptsToPlayer.Keys.First();
        foreach (KeyValuePair<int, List<int>> kvp in attemptsToPlayer)
        {
            if (kvp.Key == maxScore)
            {
                if (kvp.Key == inputAttempt.MaxAttempts)
                {
                    winnerText.text = "No winner! All players failed to solve the Barcodle. :(";
                    break;
                }
                else
                {
                    StringBuilder winnnersIndexes = new();

                    for (int i = 0; i < kvp.Value.Count; i++)
                    {
                        winnnersIndexes.Append(kvp.Value[i]);
                        if (i < kvp.Value.Count - 1)
                            winnnersIndexes.Append(", ");
                    }

                    if (kvp.Key == 1)
                        winnerText.text += $"Player(s) {winnnersIndexes}: 1 Attempt (CHEATER(s))\n";
                    else
                        winnerText.text += $"Player(s) {winnnersIndexes}: {kvp.Key} Attempts\n";
                }

                continue;
            }

            StringBuilder runnerupsIndexes = new();

            for (int i = 0; i < kvp.Value.Count; i++)
            {
                runnerupsIndexes.Append(kvp.Value[i]);
                if (i < kvp.Value.Count - 1)
                    runnerupsIndexes.Append(", ");
            }

            TMP_Text runnerup = Instantiate(runnerupPrefab, transform);
            runnerup.text = $"Player(s) {runnerupsIndexes}: ";
            runnerup.text += kvp.Key < inputAttempt.MaxAttempts
                ? $"{kvp.Key} attempts"
                : "50+ attempts (Failed) :(";
        }

        AllowExit();
    }

    void AllowExit()
    {
        canExit = true;
        exitInfoText.SetActive(true);
    }

    public void Exit()
    {
        if (!isActiveAndEnabled || !canExit)
            return;

        SceneManager.LoadScene(0);
    }
}
