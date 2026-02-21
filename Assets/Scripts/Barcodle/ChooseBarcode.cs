using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBarcode : MonoBehaviour
{
    [SerializeField] TMP_Text chosenBarcodeText;
    [SerializeField] Button confirmButton;

    const string placeholder = "--Bar-code--";

    public string ChosenBarcode => chosenBarcodeText.text;

    private void OnEnable()
    {
        chosenBarcodeText.text = placeholder;
        confirmButton.interactable = false;
    }

    public void SetChosenBarcode(string barcode)
    {
        chosenBarcodeText.text = barcode;

        confirmButton.interactable = IsBarcodeValid(barcode);
    }

    bool IsBarcodeValid(string barcode)
    {
        if (barcode.Length != 12)
            return false;

        foreach (char c in barcode)
        {
            if (!char.IsDigit(c))
                return false;
        }

        return true;
    }
}
