using EditorAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WordleCheck : MonoBehaviour
{
    [SerializeField][DisableInPlayMode] string answer;
    [SerializeField] ChooseBarcode inputBarcode;
    [SerializeField] ImageColorLerp background;
    char[] answerDigits;

    const int maxScore = 12;
    const float scorePerGreen = 1f;
    const float scorePerYellow = 0.5f;

    static readonly Color green = Color.limeGreen;
    static readonly Color yellow = Color.goldenRod;
    static readonly Color grey = Color.dimGray;

    private void OnEnable()
    {
        background.StopAllCoroutines();
        background.enabled = false;
        background.GetComponent<Image>().color = grey;

        answer = inputBarcode.ChosenBarcode;
        answerDigits = answer.ToCharArray();
    }

    public string Check(string attempt)
    {
        float score = 0;

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

            coloredAttempt[i] = ColorChar(attemptDigit, green);
            answerDigitCounts[attemptDigit]--;

            score += scorePerGreen;
        }

        for (int i = 0; i < attempt.Length; i++)
        {
            char attemptDigit = attempt[i];

            if (coloredAttempt[i] != null)
                continue;

            if (!answerDigitCounts.ContainsKey(attemptDigit) || answerDigitCounts[attemptDigit] == 0)
                continue;

            if (!answerDigits.Contains(attemptDigit))
                continue;

            coloredAttempt[i] = ColorChar(attemptDigit, yellow);
            answerDigitCounts[attemptDigit]--;

            score += scorePerYellow;
        }

        for (int i = 0; i < attempt.Length; i++)
        {
            if (coloredAttempt[i] != null)
                continue;

            coloredAttempt[i] = ColorChar(attempt[i], grey);
        }

        UpdateBackgroundColor(score);

        return string.Join("", coloredAttempt);
    }

    public bool IsCorrect(string attempt)
    {
        return attempt == answer;
    }

    string ColorChar(char c, Color color)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{c}</color>";
    }

    void UpdateBackgroundColor(float score)
    {
        float halfwayToMaxScore = maxScore / 2;
        Color bgColor;

        if (score < halfwayToMaxScore)
            bgColor = Color.Lerp(grey, yellow, score / halfwayToMaxScore);
        else
            bgColor = Color.Lerp(yellow, green, score / halfwayToMaxScore);

        background.GetComponent<Image>().color = bgColor;
    }

    public void ReEnableBackground()
    {
        background.enabled = true;
    }
}
