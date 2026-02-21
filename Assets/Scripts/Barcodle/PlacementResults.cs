using EditorAttributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlacementResults : MonoBehaviour
{
    [SerializeField] GameObject runnerupPrefab;
    [SerializeField] TMP_Text winnerText;
    [SerializeField] BarcodleManager gameManager;
    [SerializeField] InputAttempt inputAttempt;

    [Button("Calculate Placements", 36)]
    private void Start()
    {
        for (int i = 1; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);

        winnerText.text = "Winner(s):\n";

        SortedDictionary<int, List<int>> attemptsToPlayer = new();
        for (int i = 0; i < gameManager.PlayerAttempts.Length; i++)
        {
            int attempts = gameManager.PlayerAttempts[i];
            if (!attemptsToPlayer.TryGetValue(attempts, out var playerIndexes))
            {
                playerIndexes = new();
                attemptsToPlayer[attempts] = playerIndexes; // Add the new list to the SortedList
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


                    winnerText.text += $"Player(s) {winnnersIndexes}: {kvp.Key} Att.";
                }

                if (kvp.Key == 1)
                    winnerText.text += " (CHEATER)\n";
                else
                    winnerText.text += "\n";

                continue;
            }

            StringBuilder runnerupsIndexes = new();

            for (int i = 0; i < kvp.Value.Count; i++)
            {
                runnerupsIndexes.Append(kvp.Value[i]);
                if (i < kvp.Value.Count - 1)
                    runnerupsIndexes.Append(", ");
            }

            GameObject runnerup = Instantiate(runnerupPrefab, transform);
            runnerup.GetComponent<TMP_Text>().text = $"Player(s) {runnerupsIndexes}: {kvp.Key} attempts";
        }
    }
}
