using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ReadBarcode : MonoBehaviour
{
    [HideInInspector] public static ReadBarcode Instance;

    [Header("Events")]
    [SerializeField] public UnityEvent<string> OnBarcodeScanned;

    [Header("Debug")]
    [SerializeField] private InputField _InputField;
    [SerializeField] private string LastText;

    private void Awake() => Instance = this;

    private void Start()
    {
        _InputField = GetComponent<InputField>();
    }

    private void Update()
    {
        if(_InputField.text != LastText)
        {
            string barcode = _InputField.text.Length > 12 ? _InputField.text.Substring(_InputField.text.Length - 12) : _InputField.text;
            OnBarcodeScanned.Invoke(barcode);
        }

        LastText = _InputField.text;
    }
}
