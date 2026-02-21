using UnityEngine;

public class ScannableItemController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScannableItem _ScannableItem;
    [SerializeField] private SpriteRenderer _SpriteRenderer;
    [SerializeField] private CircleCollider2D _Collider;

    [Header("Floating")]
    [SerializeField] private float minPushInterval = 1f;
    [SerializeField] private float maxPushInterval = 3f;
    [SerializeField] private float pushMagnitude = 0.5f;

    private Rigidbody2D _rb;
    private float _nextPushTime;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
            _rb = gameObject.AddComponent<Rigidbody2D>();

        _rb.gravityScale = 0;
        _rb.linearDamping = 0.5f;

        ApplyRandomPush();
        ScheduleNextPush();
    }

    private void Update()
    {
        if (Time.time >= _nextPushTime)
        {
            ApplyRandomPush();
            ScheduleNextPush();
        }
    }

    private void ScheduleNextPush()
    {
        _nextPushTime = Time.time + Random.Range(minPushInterval, maxPushInterval);
    }

    private void ApplyRandomPush()
    {
        Vector2 dir = Random.insideUnitCircle.normalized;
        _rb.AddForce(dir * pushMagnitude, ForceMode2D.Impulse);
    }

    public void SetScannableItem(ScannableItem item)
    {
        _ScannableItem = item;
        _SpriteRenderer.sprite = _ScannableItem.Image;
        _Collider.radius = _ScannableItem.Radius;
    }

    public ScannableItem GetScannableItem() => _ScannableItem;
}
