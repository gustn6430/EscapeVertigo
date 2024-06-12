using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(AudioManager.SetVolume);
            volumeSlider.value = AudioManager.instance != null ? AudioManager.instance.backgroundAudioSource.volume : 0.5f;
        }
    }
}
