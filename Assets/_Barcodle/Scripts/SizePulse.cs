using UnityEngine;
using UnityEngine.UI;

public class Size : MonoBehaviour
{
    [SerializeField] private float cycleDuration = 1f; // Duration of one pulse cycle in seconds
    [SerializeField] private float minScale = 0.5f; // Minimum alpha value
    [SerializeField] private float maxScale = 1f; // Maximum alpha value
    float randomTimeOffset;

    void Start()
    {
        // Add a random time offset to desynchronize pulses if multiple objects use this script
        randomTimeOffset = Random.Range(0f, cycleDuration);
    }

    void Update()
    {
        // Calculate the scale value based on a sine wave for smooth pulsing
        float scale = (Mathf.Sin((Time.time + randomTimeOffset) * (2 * Mathf.PI / cycleDuration)) + 1) / 2 * (maxScale - minScale) + minScale;

        // Apply the calculated scale to the object's local scale
        Vector3 newScale = new Vector3(scale, scale, scale);
        transform.localScale = newScale;
    }



}
