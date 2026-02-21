using UnityEngine;

public class ScannableItemController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScannableItem _ScannableItem;
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField] private CapsuleCollider2D _Collider;

    public void SetScannableItem(ScannableItem item)
    {
        _ScannableItem = item;
        _SpriteRenderer.sprite = _ScannableItem.Image;
        _Collider.size = _ScannableItem.Size;
    }

    public ScannableItem GetScannableItem() => _ScannableItem;
}
