using TMPro;
using UnityEngine;

public class ChooseBarcode : MonoBehaviour
{
    [SerializeField] TMP_Text chosenBarcodeText;

    public string ChosenBarcode => chosenBarcodeText.text;

    public void SetChosenBarcode(string barcode)
    {
        chosenBarcodeText.text = barcode;
    }
}
