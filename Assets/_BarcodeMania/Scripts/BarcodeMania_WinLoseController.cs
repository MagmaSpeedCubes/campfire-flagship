using UnityEngine;
using UnityEngine.SceneManagement;

public class BarcodeMania_WinLoseController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private string MenuSceneName;

    public void Start()
    {
        ReadBarcode.Instance.OnBarcodeScanned.AddListener(ContinueToMenu);
    }

    public void ContinueToMenu(string _)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(MenuSceneName);
    }
}
