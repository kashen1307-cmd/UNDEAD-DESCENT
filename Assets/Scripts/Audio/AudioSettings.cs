using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioSource musicSource;
    public Slider musicSlider;

    

  

    void Start()
    {
        musicSlider.value = musicSource.volume;
    }

    public void ChangeMusicVolume()
    {
        musicSource.volume = musicSlider.value;
    }
}