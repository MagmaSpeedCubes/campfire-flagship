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
            GameObject item = Instantiate(ScannableItemPrefab);
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
