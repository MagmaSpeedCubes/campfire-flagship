using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageColorLerp : MonoBehaviour
{
    [SerializeField] Color[] lerpColors;
    [SerializeField] float speed = 0.1f;
    [SerializeField] float staticTime = 0.5f;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        StartCoroutine(LerpColors());
    }

    IEnumerator LerpColors()
    {
        image.color = lerpColors[0];

        float time = 0;
        int currentColorIndex = 0;
        while (true)
        {
            image.color = Color.Lerp(image.color, lerpColors[currentColorIndex], time);

            time += Time.deltaTime * speed;
            if (time >= staticTime)
            {
                time = 0;
                currentColorIndex = (currentColorIndex + 1) % lerpColors.Length;
            }

            yield return null;
        }
    }
}
