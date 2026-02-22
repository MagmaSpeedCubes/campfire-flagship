using UnityEngine;
using UnityEngine.UI;

public class TransparencyPulse : MonoBehaviour
{
    [SerializeField] private float cycleDuration = 1f; // Duration of one pulse cycle in seconds
    [SerializeField][Range(0f, 1f)] private float minAlpha = 0.5f; // Minimum alpha value
    [SerializeField][Range(0f, 1f)] private float maxAlpha = 1f; // Maximum alpha value
    float randomTimeOffset;

    void Start()
    {
        // Add a random time offset to desynchronize pulses if multiple objects use this script
        randomTimeOffset = Random.Range(0f, cycleDuration);
    }

    void Update()
    {
        // Calculate the alpha value based on a sine wave for smooth pulsing
        float alpha = (Mathf.Sin((Time.time + randomTimeOffset) * (2 * Mathf.PI / cycleDuration)) + 1) / 2 * (maxAlpha - minAlpha) + minAlpha;

        // Apply the calculated alpha to the material's color
        Color color = GetComponent<Image>().color;
        color.a = alpha;
        GetComponent<Image>().color = color;
    }



}
