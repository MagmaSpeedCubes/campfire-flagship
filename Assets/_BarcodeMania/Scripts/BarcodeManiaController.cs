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
            int randIndex = Random.Range(0, _ScannableItems.Length);
            controller.SetScannableItem(_ScannableItems[randIndex]);
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
        bool found = false;
        for (int i = 0; i < SpawnedItems.Count; i++)
        {
            GameObject item = SpawnedItems[i];
            ScannableItemController controller = item.GetComponent<ScannableItemController>();
            if (controller.GetScannableItem().Barcode == long.Parse(barcode))
            {
                SpawnedItems.RemoveAt(i);
                Destroy(item);
                found = true;
                break;
            }
        }
        if (!found)
        {
            TimeLimit -= 5f;
        }
    }

    private void UpdateUI()
    {
        TimeText.text = $"Time Remaining: {Mathf.CeilToInt(TimeLimit)}s";
    }
}
