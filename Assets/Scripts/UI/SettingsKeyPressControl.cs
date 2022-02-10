using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsKeyPressControl : MonoBehaviour
{
    public AudioMixerGroup mixer;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider soundsSlider;
    private void Awake()
    {
        ApplySaveSettings();
    }

    private void ApplySaveSettings()
    {
        mixer.audioMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("masterVolume"));
        mixer.audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
        mixer.audioMixer.SetFloat("soundsVolume", PlayerPrefs.GetFloat("soundsVolume"));

        masterSlider.value = (PlayerPrefs.GetFloat("masterVolume") + 80) / 100;
        musicSlider.value = (PlayerPrefs.GetFloat("musicVolume") + 80) / 100;
        soundsSlider.value = (PlayerPrefs.GetFloat("soundsVolume") + 80) / 100;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSettings();
        }
    }

    public void SliderMasterChanged()
    {
        mixer.audioMixer.SetFloat("masterVolume", (masterSlider.value * 100) - 80);
    }

    public void SliderMusicChanged()
    {
        mixer.audioMixer.SetFloat("musicVolume", (musicSlider.value * 100) - 80);
    }

    public void SliderSoundsChanged()
    {
        mixer.audioMixer.SetFloat("soundsVolume", (soundsSlider.value * 100) - 80);
    }

    public void CloseSettings()
    {
        SaveFieldsToStaticClass();
        gameObject.SetActive(false);
    }

    private void SaveFieldsToStaticClass()
    {
        PlayerPrefs.SetFloat("masterVolume", (masterSlider.value * 100) - 80);
        PlayerPrefs.SetFloat("musicVolume", (musicSlider.value * 100) - 80);
        PlayerPrefs.SetFloat("soundsVolume", (soundsSlider.value * 100) - 80);
    }
}
