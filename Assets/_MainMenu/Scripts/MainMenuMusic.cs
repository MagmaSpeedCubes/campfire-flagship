using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MainMenuMusic : MonoBehaviour
{
    [SerializeField] AudioClip defaultMusic;
    [SerializeField] AudioClip specialMusic;
    [SerializeField][Range(0.001f, 1f)] float specialMusicChance = 0.1f;

    private void Awake()
    {
        var audioSource = GetComponent<AudioSource>();
        if (Random.value <= specialMusicChance)
            audioSource.clip = specialMusic;
        else
            audioSource.clip = defaultMusic;

        audioSource.Play();
    }
}