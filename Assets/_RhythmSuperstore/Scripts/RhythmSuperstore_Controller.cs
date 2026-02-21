using System.Collections.Generic;
using UnityEngine;

public class RhythmSuperstore_Controller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<RhythmSuperstore_Item> _Items;
    [SerializeField] private GameObject ItemPrefab;
    [SerializeField] private Transform SpawnPosition;
 
    [Header("Settings")]
    [SerializeField] private float SpawnInterval;

    [Header("Debug")]
    [SerializeField] private List<GameObject> SpawnedItems;
    [SerializeField] private float SpawnCounter;
    [SerializeField] private int Score;

    private void Start()
    {
        ReadBarcode.Instance.OnBarcodeScanned.AddListener(OnItemScanned);
    }

    private void Update()
    {
        CheckForDeletedItems();
        HandleSpawning();
    }

    private void HandleSpawning()
    {
        SpawnCounter -= Time.deltaTime;
        if (SpawnCounter <= 0)
        {
            SpawnCounter = SpawnInterval;
            SpawnItem();
        }
    }

    private void CheckForDeletedItems()
    {
        for (int i = SpawnedItems.Count - 1; i >= 0; i--)
        {
            if (SpawnedItems[i] == null)
            {
                SpawnedItems.RemoveAt(i);
            }
        }
    }

    public void OnItemScanned(string barcode)
    {
        Camera mainCamera = Camera.main;
        // First, try to scan items visible in camera
        foreach (GameObject item in SpawnedItems)
        {
            if (item == null) continue;
            Vector3 viewportPos = mainCamera.WorldToViewportPoint(item.transform.position);
            bool isVisible = viewportPos.z > 0 && viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1;
            if (!isVisible) continue;
            RhythmSuperstore_ItemController controller = item.GetComponent<RhythmSuperstore_ItemController>();
            if (controller.GetItem().Barcode == long.Parse(barcode))
            {
                SpawnedItems.Remove(item);
                Destroy(item);
                Score ++;
                return;
            }
        }
        // If not found, fallback to any item
        foreach (GameObject item in SpawnedItems)
        {
            if (item == null) continue;
            RhythmSuperstore_ItemController controller = item.GetComponent<RhythmSuperstore_ItemController>();
            if (controller.GetItem().Barcode == long.Parse(barcode))
            {
                SpawnedItems.Remove(item);
                Destroy(item);
                Score ++;
                return;
            }
        }
    }

    private void SpawnItem()
    {
        GameObject itemObj = Instantiate(ItemPrefab, SpawnPosition.position, Quaternion.identity, transform);
        RhythmSuperstore_ItemController controller = itemObj.GetComponent<RhythmSuperstore_ItemController>();
        int randIndex = Random.Range(0, _Items.Count);
        controller.SetItem(_Items[randIndex]);
        SpawnedItems.Add(itemObj);
    }
}
