using TMPro;
using UnityEngine;

public class InputAttempt : MonoBehaviour
{
    [SerializeField] TMP_Text attemptPrefab;
    [SerializeField] Transform scrollViewContent;

    const long testAnswer = 123_456_789_012;

    public void AddAttempt(string attempt)
    {
        TMP_Text newAttempt = Instantiate(attemptPrefab, scrollViewContent);
        newAttempt.text = attempt;
    }
}
