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
        _InputField.Select();
    }
}
