using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioSource musicSource;
    public Slider musicSlider;

    public AudioMixer audioMixer;

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20f);
    }


    void Start()
    {
        musicSlider.value = musicSource.volume;

        SetSFXVolume(1f);
    }

    public void ChangeMusicVolume()
    {
        musicSource.volume = musicSlider.value;
    }
}