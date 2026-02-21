using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class RainbowTextLoop : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;
    TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        float hue = (Time.time * speed) % 1f;
        text.color = Color.HSVToRGB(hue, 1f, 1f);
    }
}
