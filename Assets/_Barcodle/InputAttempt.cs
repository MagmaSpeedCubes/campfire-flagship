using TMPro;
using UnityEngine;

public class InputAttempt : MonoBehaviour
{
    [SerializeField] int attempts, maxAttempts;
    [SerializeField] TMP_Text attemptPrefab, attemptCountText;
    [SerializeField] Transform scrollViewContent;
    [SerializeField] WordleCheck answerCheck;
    [SerializeField] BarcodleManager gameManager;

    public int MaxAttempts => maxAttempts;

    public void AddAttempt(string attempt)
    {
        if (!isActiveAndEnabled)
            return;

        Increment();

        TMP_Text newAttempt = Instantiate(attemptPrefab, scrollViewContent);
        newAttempt.text = answerCheck.Check(attempt);

        if (answerCheck.IsCorrect(attempt))
        {
            if (attempts == 1)
                gameManager.DisplayStatus($"You solved the Barcodle in 1 attempt! CHEATER", Color.forestGreen, attempts);
            else
                gameManager.DisplayStatus($"You solved the Barcodle in {attempts} attempts!", Color.forestGreen, attempts);
        }
    }

    void Increment()
    {
        attempts++;
        UpdateAttemptsText();

        if (attempts >= maxAttempts)
            gameManager.DisplayStatus("Failed! Too many attempts. :(", Color.firebrick, attempts);
    }

    void UpdateAttemptsText()
    {
        attemptCountText.text = $"Attempts: {attempts}/{maxAttempts} Max";
    }

    public void NewRound()
    {
        foreach (Transform child in scrollViewContent)
            Destroy(child.gameObject);

        attempts = 0;
        UpdateAttemptsText();
    }
}
