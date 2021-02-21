using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject OptionsMenu = null;

    [SerializeField] AudioMixerGroup MusicAudioMixerGroup;
    [SerializeField] AudioMixerGroup SFXAudioMixerGroup;

    [SerializeField] Slider MusicVolumeSlider = null;
    [SerializeField] Slider SFXVolumeSlider = null;

    [SerializeField] TextMeshProUGUI MusicVolumeSliderPercentText = null;
    [SerializeField] TextMeshProUGUI SFXVolumeSliderPercentText = null;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        MusicAudioMixerGroup.audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        SFXAudioMixerGroup.audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
    }

    public void StartPlaying()
    {
        AudioManager.instance.Stop("MenuMusic");
        SceneManager.LoadScene("Arena", LoadSceneMode.Single);
    }

    public void ToggleOptionsMenu()
    {
        OptionsMenu.SetActive(!OptionsMenu.activeSelf);
    }

    public void UpdateSliderText()
    {
        UpdateAudioMixerGroupVolumes(MusicVolumeSlider.value, SFXVolumeSlider.value);

        MusicVolumeSliderPercentText.text = (int)(MusicVolumeSlider.value * 100) + "%";
        SFXVolumeSliderPercentText.text = (int)(SFXVolumeSlider.value * 100) + "%";
    }

    public void UpdateAudioMixerGroupVolumes(float musicVolumeValue, float sfxVolumeValue)
    {
        MusicAudioMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolumeValue) * 20);
        SFXAudioMixerGroup.audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolumeValue) * 20);

        PlayerPrefs.SetFloat("MusicVolume", Mathf.Log10(musicVolumeValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", Mathf.Log10(sfxVolumeValue) * 20);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
