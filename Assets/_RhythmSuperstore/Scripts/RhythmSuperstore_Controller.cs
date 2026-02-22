using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RhythmSuperstore_Controller : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<RhythmSuperstore_Item> _Items;
    [SerializeField] private GameObject ItemPrefab;
    [SerializeField] private Transform SpawnPosition;
    [SerializeField] private Text _Text;
    [SerializeField] private TextMeshProUGUI _EventText;
    [SerializeField] private Transform beltEnd;
 
    [Header("Settings")]
    [SerializeField] private int AmountToSpawn;
    [SerializeField] private float minSpawnTime = 0.25f;
    [SerializeField] private float maxSpawnTime = 2f;
    [SerializeField] private string EndSceneName;
    [SerializeField] private int maxScore = 10;
    [SerializeField] private Dictionary<string, Color> scoreColors = new Dictionary<string, Color>()
    {
        { "Perfect", Color.green },
        { "Good", Color.yellow },
        { "OK",Color.white }, 
        { "Miss", Color.red }
    };

    [Header("Debug")]
    [SerializeField] private List<GameObject> SpawnedItems;
    [SerializeField] private float SpawnCounter;
    [SerializeField] private int Score;

    private void Start()
    {
        ReadBarcode.Instance.OnBarcodeScanned.AddListener(OnItemScanned);
        StartCoroutine(SpawnItems());
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
        List<GameObject> matchingItems = new List<GameObject>();
        foreach (GameObject item in SpawnedItems)
        {
            if (item == null) continue;
            Vector3 viewportPos = mainCamera.WorldToViewportPoint(item.transform.position);
            bool isVisible = viewportPos.z > 0 && viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1;
            if (!isVisible) continue;
            RhythmSuperstore_ItemController controller = item.GetComponent<RhythmSuperstore_ItemController>();
            if (controller.GetItem().Barcode == long.Parse(barcode))
            {
                matchingItems.Add(item);
            }
        }
        if(matchingItems.Count == 0)
        {
            Score -= 1;//Penalty for wrong scan
            _EventText.text = "Miss (-1)";
            _EventText.color = scoreColors["Miss"];
            return;
        }
        GameObject closestItem = null;
        foreach (GameObject item in matchingItems)
        {
            float xDistance = Mathf.Abs(beltEnd.transform.position.x - item.transform.position.x);
            float closestDistance = closestItem == null ? float.MaxValue : Mathf.Abs(beltEnd.transform.position.x - closestItem.transform.position.x);
            if (closestItem == null || xDistance < closestDistance && xDistance > 0f)
            {
                closestItem = item;
            }
        }
        float beltClosestXDistance = Mathf.Abs(beltEnd.transform.position.x - closestItem.transform.position.x);
        if(beltClosestXDistance < 0)
        {
            Score -= 2;//Higher penalty for scanning item too late
            _EventText.text = "Too late (-2)";
            _EventText.color = scoreColors["Miss"];
            return;
        }
        if(beltClosestXDistance > 10f)
        {
            Score -= 1;//Penalty for scanning item too early
            _EventText.text = "Too early (-1)";
            _EventText.color = scoreColors["Miss"];
            return;
        }
        int addScore = (int)Mathf.Clamp(maxScore / Mathf.Sqrt(beltClosestXDistance), 1, maxScore);
        if(addScore == maxScore) {_EventText.text = "Perfect (+" + addScore + ")";
            _EventText.color = scoreColors["Perfect"];
        }
        else if(addScore > maxScore * 0.5f){ _EventText.text = "Good (+" + addScore + ")";
            _EventText.color = scoreColors["Good"];
        }
        else {_EventText.text = "OK! (+" + addScore + ")";
            _EventText.color = scoreColors["OK"];
        }


        SpawnedItems.Remove(closestItem);
        Destroy(closestItem);
        Score += addScore;
        Debug.Log($"Scanned item with barcode {barcode}. Distance to belt end: {beltClosestXDistance}. Score added: {addScore}");
        return;
        



        // foreach (GameObject item in SpawnedItems)
        // {
        //     if (item == null) continue;
        //     RhythmSuperstore_ItemController controller = item.GetComponent<RhythmSuperstore_ItemController>();
        //     if (controller.GetItem().Barcode == long.Parse(barcode))
        //     {
        //         SpawnedItems.Remove(item);
        //         Destroy(item);
        //         Score ++;
        //         return;
        //     }
        // }
    }

    private void UpdateIUI()
    {
        _Text.text = $"Score: {Score}";
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

    IEnumerator SpawnItems()
    {
        for (int i = 0; i < AmountToSpawn; i++)
        {
            int randIndex = Random.Range(0, _Items.Count);
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            GameObject itemObj = Instantiate(ItemPrefab, SpawnPosition.position + randomOffset, Quaternion.identity, transform);
            RhythmSuperstore_ItemController controller = itemObj.GetComponent<RhythmSuperstore_ItemController>();
            controller.SetItem(_Items[randIndex]);
            SpawnedItems.Add(itemObj);
            float spawnDelay = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void GameOverCondition()
    {
        RhythmSuperstore_GameData.Instance.Got = Score;
        RhythmSuperstore_GameData.Instance.OutOf = AmountToSpawn;
        UnityEngine.SceneManagement.SceneManager.LoadScene(EndSceneName);
    }
}
