using UnityEngine;

[CreateAssetMenu(fileName = "ScannableItem", menuName = "Barcode Mania/ScannableItem")]
public class ScannableItem : ScriptableObject
{
    [Header("Settings")]
    public string Name;
    public long Barcode;
    public Sprite Image;

    [Header("Collider")]
    public Vector2 Size;
}
