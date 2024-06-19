using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeCount : MonoBehaviour
{
    public float seconds;
    public float timerDuration;
    public TextMeshProUGUI text;
    public GameObject GameOver;
    public AudioSource timeSound;
    public AudioSource backGroundMusic;
    
    void Start()
    {
        if (timeSound == null && backGroundMusic == null)
        {
            timeSound = GetComponent<AudioSource>();
            backGroundMusic = GetComponent<AudioSource>();
        }
        seconds = timerDuration;

    }

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        seconds -= Time.deltaTime;

        if (seconds <= 0)
        {
            seconds = 0;
            timeSound.mute = true;
            backGroundMusic.mute = true;
        }

        if (seconds == 0)
        {
            GameOver.SetActive(true);
            
            Time.timeScale = 0.0f;
        }

        text.text = string.Format("{0:N2}", seconds);
    }
}