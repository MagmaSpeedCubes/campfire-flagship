using UnityEngine;
using UnityEngine.UI;

public class KeepTextboxFocus : MonoBehaviour
{
    [SerializeField] private InputField _InputField;

    void Start()
    {
        _InputField = GetComponent<InputField>();
    }

    void Update()
    {
        if (!_InputField.isFocused)
        {
            _InputField.ActivateInputField();
        }

        int endPos = _InputField.text.Length;
        _InputField.caretPosition = endPos;
        _InputField.selectionAnchorPosition = endPos;
        _InputField.selectionFocusPosition = endPos;
    }
}