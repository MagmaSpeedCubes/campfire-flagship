using UnityEngine;
using UnityEngine.UI;
public class UIBar : MonoBehaviour
{
[SerializeField] private RectTransform barPrefab;
    [SerializeField] private float min, max;
    float value { get; set; }
    public void SetBarValue(float newValue)
    {
        value = Mathf.Clamp(newValue, min, max);
        barPrefab.localScale = new Vector2(((value - min) / (max - min)), barPrefab.localScale.y);
        SetColorBasedOnPercentage(value / max);
    }

    void SetColorBasedOnPercentage(float percentage)
    {
        if(percentage > 0.5f)
        {
            barPrefab.GetComponent<Image>().color = Color.white;
        }
        else if(percentage > 0.25f)
        {
            barPrefab.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            barPrefab.GetComponent<Image>().color = Color.red;
        }
    }
}