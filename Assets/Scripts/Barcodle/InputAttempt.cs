using TMPro;
using UnityEngine;

public class InputAttempt : MonoBehaviour
{
    [SerializeField] int attempts, maxAttempts;
    [SerializeField] TMP_Text attemptPrefab, attemptCountText;
    [SerializeField] Transform scrollViewContent;
    [SerializeField] WordleCheck answerCheck;

    public void AddAttempt(string attempt)
    {
        if (!isActiveAndEnabled)
            return;

        Increment();

        TMP_Text newAttempt = Instantiate(attemptPrefab, scrollViewContent);
        newAttempt.text = answerCheck.Check(attempt);
    }

    void Increment()
    {
        attempts++;
        attemptCountText.text = $"Attempts: {attempts}/{maxAttempts} Max";

        if (attempts >= maxAttempts)
        {
            Debug.Log("Too many attempts!");
            //add text  
        }
    }
}
