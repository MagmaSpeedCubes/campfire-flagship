using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RhythmSuperstoreManager : MonoBehaviour
{
    [SerializeField]private GameObject[] itemPrefabs;
    [SerializeField]private Transform spawnPoint;
    int wave = 1;
    int health = 50;
    Coroutine spawnCoroutine;

    public void StartGame()
    {
        wave = 1;
        spawnCoroutine = StartCoroutine(SpawnWave(wave));
    }

    public void EndGame()
    {
        StopCoroutine(spawnCoroutine);
        
    }
    IEnumerator SpawnWave(int difficulty)
    {
        float timeElapsed = 0f;
        while (timeElapsed < difficulty * 10f)
        {
            float spawnRate = Mathf.Log(difficulty + 1) * 0.5f; 

            
            int randomIndex = Random.Range(0, itemPrefabs.Length);
            Instantiate(itemPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(1/spawnRate);
            timeElapsed += 1/spawnRate;
        }
        wave++;
        spawnCoroutine = StartCoroutine(SpawnWave(wave));
        yield break;
    }
}
