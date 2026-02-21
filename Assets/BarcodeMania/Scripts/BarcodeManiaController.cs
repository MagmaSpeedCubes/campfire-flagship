using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarcodeManiaController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject ScannableItemPrefab;
    [SerializeField] private ScannableItem[] _ScannableItems;
    [SerializeField] private Text TimeText;
    [Space]
    [SerializeField] private string WinSceneName;
    [SerializeField] private string LoseSceneName;

    [Header("Settings")]
    [SerializeField] private int NumberOfItemsToSpawn;

    [Header("Debug")]
    [SerializeField] private List<GameObject> SpawnedItems;
    [SerializeField] private float TimeLimit = 60;

    private void Start()
    {
        ReadBarcode.Instance.OnBarcodeScanned.AddListener(OnItemScanned);

        SpawnedItems = new List<GameObject>();
        for (int i = 0; i < NumberOfItemsToSpawn; i++)
        {
            Vector3 basePos = transform.position;
            float offsetX = Random.Range(-1f, 1f);
            float offsetY = Random.Range(-1f, 1f);
            Vector3 spawnPos = new Vector3(basePos.x + offsetX, basePos.y + offsetY, basePos.z);
            GameObject item = Instantiate(ScannableItemPrefab, spawnPos, Quaternion.identity, transform);
            ScannableItemController controller = item.GetComponent<ScannableItemController>();
            controller.SetScannableItem(_ScannableItems[i % _ScannableItems.Length]);
            SpawnedItems.Add(item);
        }
    }

    private void Update()
    {
        TimeLimit -= Time.deltaTime;
        UpdateUI();

        WinCondition();
        LoseCondition();
    }

    private void WinCondition()
    {
        if (SpawnedItems.Count == 0) UnityEngine.SceneManagement.SceneManager.LoadScene(WinSceneName);
    }

    private void LoseCondition()
    {
        if (TimeLimit <= 0) UnityEngine.SceneManagement.SceneManager.LoadScene(LoseSceneName);
    }

    public void OnItemScanned(string barcode)
    {
        if(barcode.Length < 12) return;

        foreach (GameObject item in SpawnedItems)
        {
            ScannableItemController controller = item.GetComponent<ScannableItemController>();
            if (controller.GetScannableItem().Barcode == long.Parse(barcode))
            {
                SpawnedItems.Remove(item);
                Destroy(item);
                return;
            }
        }
    }

    private void UpdateUI()
    {
        TimeText.text = $"Time Remaining: {Mathf.CeilToInt(TimeLimit)}s";
    }
}
