using UnityEngine;

public class ScannableItemController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScannableItem _ScannableItem;
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField] private CircleCollider2D _Collider;

    public void SetScannableItem(ScannableItem item)
    {
        _ScannableItem = item;
        _SpriteRenderer.sprite = _ScannableItem.Image;
        _Collider.radius = _ScannableItem.Radius;
    }

    public ScannableItem GetScannableItem() => _ScannableItem;
}
