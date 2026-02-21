using System.Collections.Generic;
using UnityEngine;

public class BarcodeManiaController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject ScannableItemPrefab;
    [SerializeField] private ScannableItem[] _ScannableItems;

    [Header("Settings")]
    [SerializeField] private int NumberOfItemsToSpawn;

    [Header("Debug")]
    [SerializeField] private List<GameObject> SpawnedItems;

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
            GameObject item = Instantiate(ScannableItemPrefab, spawnPos, Quaternion.identity);
            ScannableItemController controller = item.GetComponent<ScannableItemController>();
            controller.SetScannableItem(_ScannableItems[i % _ScannableItems.Length]);
            SpawnedItems.Add(item);
        }
    }

    public void OnItemScanned(string barcode)
    {
        Debug.Log($"Scanned barcode: {barcode}");
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
}
