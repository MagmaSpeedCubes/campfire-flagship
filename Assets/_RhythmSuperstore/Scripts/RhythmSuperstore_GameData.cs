using UnityEngine;

public class RhythmSuperstore_GameData : MonoBehaviour
{
    [HideInInspector] public static RhythmSuperstore_GameData Instance;

    [SerializeField] public int Got;
    [SerializeField] public int OutOf;

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