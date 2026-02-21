using TMPro;
using UnityEngine;

public class InputAttempt : MonoBehaviour
{
    [SerializeField] int attempts, maxAttempts;
    [SerializeField] TMP_Text attemptPrefab, attemptCountText;
    [SerializeField] Transform scrollViewContent;
    [SerializeField] WordleCheck answerCheck;
    [SerializeField] BarcodleManager gameManager;

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
        attemptCountText.text = $"Attempts: {attempts}/{maxAttempts} Max";

        if (attempts >= maxAttempts)
            gameManager.DisplayStatus("Failed! Too many attempts. :(", Color.firebrick, attempts);
    }
}
