using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ReadBarcode : MonoBehaviour
{
    [HideInInspector] public static ReadBarcode Instance;

    [SerializeField] private InputField _InputField;
    [SerializeField] private string LastText;

    [SerializeField] public UnityEvent<string> OnBarcodeScanned;

    private void Awake() => Instance = this;

    private void Start()
    {
        _InputField = GetComponent<InputField>();
    }

    private void Update()
    {
        if(LastText != _InputField.text && _InputField.text.Length == 12)
        {
            OnBarcodeScanned.Invoke(_InputField.text);
            _InputField.text = string.Empty;
        }
        LastText = _InputField.text;
    }
}
