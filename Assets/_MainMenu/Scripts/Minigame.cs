using UnityEngine;

[CreateAssetMenu(fileName = "Minigame", menuName = "Main Menu/Minigame")]
public class Minigame : ScriptableObject
{
    [Header("Settings")]
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField] public string SceneName;
    [SerializeField] public Sprite Thumbnail;
}
