using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ReadBarcode : MonoBehaviour
{
    [HideInInspector] public static ReadBarcode Instance;

    [SerializeField] private InputField _InputField;

    [SerializeField] public UnityEvent<string> OnBarcodeScanned;

    private void Awake() => Instance = this;

    private void Start() => _InputField = GetComponent<InputField>();

    public void OnTextChanged(string text)
    {
        if (text.Length < 12) return;
        OnBarcodeScanned.Invoke(text);
        _InputField.text = string.Empty;
    }
}