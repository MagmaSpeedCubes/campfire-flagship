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
    [SerializeField] private float SpawnCounter;

    private void Update()
    {
        SpawnCounter -= Time.deltaTime;
        if (SpawnCounter <= 0)
        {
            SpawnCounter = SpawnInterval;
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        GameObject itemObj = Instantiate(ItemPrefab, SpawnPosition.position, Quaternion.identity, transform);
        RhythmSuperstore_ItemController controller = itemObj.GetComponent<RhythmSuperstore_ItemController>();
        int randIndex = Random.Range(0, _Items.Count);
        controller.SetItem(_Items[randIndex]);
    }
}
