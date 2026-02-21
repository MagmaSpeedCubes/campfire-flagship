using EditorAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordleCheck : MonoBehaviour
{
    [SerializeField][DisableInPlayMode] string answer;
    [SerializeField] ChooseBarcode inputBarcode;
    char[] answerDigits;

    private void Awake()
    {
        answer = inputBarcode.ChosenBarcode;
        answerDigits = answer.ToCharArray();
    }

    public string Check(string attempt)
    {
        Dictionary<char, int> answerDigitCounts = new();
        foreach (char c in answerDigits)
        {
            if (answerDigitCounts.ContainsKey(c))
                answerDigitCounts[c]++;
            else
                answerDigitCounts[c] = 1;
        }

        string[] coloredAttempt = new string[attempt.Length];

        for (int i = 0; i < attempt.Length; i++)
        {
            char attemptDigit = attempt[i];

            if (attemptDigit != answerDigits[i])
                continue;

            coloredAttempt[i] = ColorChar(attemptDigit, Color.limeGreen);
            answerDigitCounts[attemptDigit]--;
        }

        for (int i = 0; i < attempt.Length; i++)
        {
            char attemptDigit = attempt[i];

            if (coloredAttempt[i] != null || answerDigitCounts[attemptDigit] == 0 || !answerDigits.Contains(attemptDigit))
                continue;

            coloredAttempt[i] = ColorChar(attemptDigit, Color.gold);
            answerDigitCounts[attemptDigit]--;
        }

        for (int i = 0; i < attempt.Length; i++)
        {
            if (coloredAttempt[i] != null)
                continue;

            coloredAttempt[i] = ColorChar(attempt[i], Color.dimGray);
        }

        return string.Join("", coloredAttempt);
    }

    string ColorChar(char c, Color color)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{c}</color>";
    }
}
