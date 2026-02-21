using TMPro;
using UnityEngine;

public class InputAttempt : MonoBehaviour
{
    [SerializeField] TMP_Text attemptPrefab;
    [SerializeField] Transform scrollViewContent;
    [SerializeField] WordleCheck answerCheck;

    public void AddAttempt(string attempt)
    {
        TMP_Text newAttempt = Instantiate(attemptPrefab, scrollViewContent);
        newAttempt.text = answerCheck.Check(attempt);
    }
}
