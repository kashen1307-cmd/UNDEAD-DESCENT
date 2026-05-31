using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioSettings : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public AudioMixer audioMixer;

    private AudioSource musicSource;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        FindMusicSource();

        if (musicSlider != null && musicSource != null)
        {
            musicSlider.value = musicSource.volume;
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindMusicSource();

        if (musicSlider == null)
        {
            musicSlider =
                GameObject.Find("MusicSlider")
                ?.GetComponent<Slider>();
        }

        if (sfxSlider == null)
        {
            sfxSlider =
                GameObject.Find("SFXSlider")
                ?.GetComponent<Slider>();
        }
    }

    void FindMusicSource()
    {
        if (MusicManager.instance != null)
        {
            musicSource =
                MusicManager.instance
                .GetComponent<AudioSource>();
        }
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(
            "SFXVolume",
            Mathf.Log10(volume) * 20f);
    }

    public void ChangeMusicVolume()
    {
        FindMusicSource();

        if (musicSource != null &&
            musicSlider != null)
        {
            musicSource.volume =
                musicSlider.value;
        }
    }
}