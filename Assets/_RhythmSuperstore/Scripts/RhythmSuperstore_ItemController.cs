using UnityEngine;

public class RhythmSuperstore_ItemController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField] private CapsuleCollider2D _Collider;
    
    [Header("Debug")]
    [SerializeField] private RhythmSuperstore_Item _Item;

    public void SetItem(RhythmSuperstore_Item item)
    {
        _Item = item;
        _SpriteRenderer.sprite = _Item.Image;
        _Collider.size = _Item.Size;
    }
}
