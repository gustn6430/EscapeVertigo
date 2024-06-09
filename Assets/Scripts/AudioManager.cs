using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;  // instance 필드를 public으로 변경
    public AudioSource audioSource;

    public AudioClip backgroundMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void SetVolume(float volume)
    {
        if (instance != null && instance.audioSource != null)
        {
            instance.audioSource.volume = volume;
        }
    }
}
