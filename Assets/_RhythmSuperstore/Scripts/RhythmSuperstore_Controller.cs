using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RhythmSuperstore_Controller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<RhythmSuperstore_Item> _Items;
    [SerializeField] private GameObject ItemPrefab;
    [SerializeField] private Transform SpawnPosition;
    [SerializeField] private Text _Text;
 
    [Header("Settings")]
    [SerializeField] private int AmountToSpawn;
    [SerializeField] private string EndSceneName;

    [Header("Debug")]
    [SerializeField] private List<GameObject> SpawnedItems;
    [SerializeField] private float SpawnCounter;
    [SerializeField] private int Score;

    private void Start()
    {
        ReadBarcode.Instance.OnBarcodeScanned.AddListener(OnItemScanned);
        SpawnAllItems();
    }

    private void Update()
    {
        CheckForDeletedItems();
        UpdateIUI();
        if (SpawnedItems.Count <= 0) GameOverCondition();
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

    private void UpdateIUI()
    {
        _Text.text = $"Score: {Score}/{AmountToSpawn}";
    }

    private void SpawnAllItems()
    {
        SpawnedItems.Clear();
        for (int i = 0; i < AmountToSpawn; i++)
        {
            int randIndex = Random.Range(0, _Items.Count);
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            GameObject itemObj = Instantiate(ItemPrefab, SpawnPosition.position + randomOffset, Quaternion.identity, transform);
            RhythmSuperstore_ItemController controller = itemObj.GetComponent<RhythmSuperstore_ItemController>();
            controller.SetItem(_Items[randIndex]);
            SpawnedItems.Add(itemObj);
        }
    }

    private void GameOverCondition()
    {
        RhythmSuperstore_GameData.Instance.Got = Score;
        RhythmSuperstore_GameData.Instance.OutOf = AmountToSpawn;
        UnityEngine.SceneManagement.SceneManager.LoadScene(EndSceneName);
    }
}
