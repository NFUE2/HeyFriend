using UnityEngine;

public class GameOverAudio : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void OnEnable()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
