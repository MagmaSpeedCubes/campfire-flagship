using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ReadBarcode : MonoBehaviour
{
    [HideInInspector] public static ReadBarcode Instance;

    [SerializeField] private InputField _InputField;
    [SerializeField] private string LastText;

    [SerializeField] private UnityEvent<string> OnBarcodeScanned;

    private void Awake() => Instance = this;

    private void Start()
    {
        _InputField = GetComponent<InputField>();
    }

    private void Update()
    {
        if(LastText != _InputField.text && _InputField.text.Length == 12)
        {
            WhatItemWasScanned(_InputField.text);
            _InputField.text = string.Empty;
        }
        LastText = _InputField.text;
    }

    private void WhatItemWasScanned(string text)
    {
        OnBarcodeScanned.Invoke(text);

        switch (text)
        {
            case "049000061062":
                Debug.Log("You scanned a can of Sprite!");
                break;
            case "889392021394":
                Debug.Log("You scanned a can of Celsius!");
                break;
            default:
                Debug.Log("Unknown item scanned.");
                break;
        }
    }
}
