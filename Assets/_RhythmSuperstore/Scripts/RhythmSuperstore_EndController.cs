using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RhythmSuperstore_EndController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Text _Text;

    [Header("Settings")]
    [SerializeField] private string MenuSceneName;

    private void Start()
    {
        _Text.text = $"Final Score: {RhythmSuperstore_GameData.Instance.Got}/{RhythmSuperstore_GameData.Instance.OutOf}";
    
        ReadBarcode.Instance.OnBarcodeScanned.AddListener(OnScan);
    }

    public void OnScan(string _)
    {
        SceneManager.LoadScene(MenuSceneName);
    }
}
