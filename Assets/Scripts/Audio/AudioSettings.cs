using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioSettings : MonoBehaviour
{
    public AudioSource musicSource;
    public Slider musicSlider;
    public AudioMixer audioMixer;
    public Slider sfxSlider;

    void Start()
    {
        musicSlider.value = musicSource.volume;

        sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Slider sfxSlider = GameObject.Find("SFXSlider")?.GetComponent<Slider>();

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(
        "SFXVolume",
        Mathf.Log10(
        Mathf.Max(volume, 0.0001f)) * 20f);
    }

    public void ChangeMusicVolume()
    {
        musicSource.volume = musicSlider.value;
    }
}