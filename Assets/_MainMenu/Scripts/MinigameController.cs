using UnityEngine;
using UnityEngine.UI;

public class MinigameController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Minigame _Minigame;
    [SerializeField] private SpriteRenderer ThumbnailRenderer;
    [SerializeField] private Text TitleText;

    public void SetMinigame(Minigame _minigame)
    {
        _Minigame = _minigame;

        ThumbnailRenderer.sprite = _Minigame.Thumbnail;
        TitleText.text = _Minigame.Name;
    }
}
