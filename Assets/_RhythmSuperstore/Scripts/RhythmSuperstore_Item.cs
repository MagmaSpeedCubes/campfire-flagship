using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Rhythm Superstore/Item")]
public class RhythmSuperstore_Item : ScriptableObject
{
    [Header("Settings")]
    public long Barcode = 000000000000;
    public Sprite Image;

    [Header("Collider")]
    public Vector2 Size = new Vector2(0.5f, 1);
}

