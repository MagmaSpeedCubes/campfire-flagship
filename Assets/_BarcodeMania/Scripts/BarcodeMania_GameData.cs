using System.Collections.Generic;
using UnityEngine;

public class BarcodeMania_GameData : MonoBehaviour
{
    [HideInInspector] public static BarcodeMania_GameData Instance;

    [SerializeField] public List<float> PlayerScores;
    [SerializeField] public List<float> PlayerTimes;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}