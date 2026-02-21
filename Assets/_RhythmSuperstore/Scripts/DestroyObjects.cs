using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision) => Destroy(collision.gameObject);
}
