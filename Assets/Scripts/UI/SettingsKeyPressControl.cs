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
    public Slider menuMusicSlider;
    public Slider soundsSlider;

    private void Start()
    {
        ApplySaveSettings();
    }

    public void ApplySaveSettings()
    {
        mixer.audioMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("masterVolume"));
        mixer.audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
        mixer.audioMixer.SetFloat("menuMusicVolume", PlayerPrefs.GetFloat("menuMusicVolume"));
        mixer.audioMixer.SetFloat("soundsVolume", PlayerPrefs.GetFloat("soundsVolume"));

        masterSlider.value = (PlayerPrefs.GetFloat("masterVolume") + 80) / 80;
        musicSlider.value = (PlayerPrefs.GetFloat("musicVolume") + 80) / 80;
        menuMusicSlider.value = (PlayerPrefs.GetFloat("menuMusicVolume") + 80) / 80;
        soundsSlider.value = (PlayerPrefs.GetFloat("soundsVolume") + 80) / 80;

        Vector3 vec = GetComponent<RectTransform>().position;
        vec.z = 0;
        GetComponent<RectTransform>().position = vec;
        gameObject.SetActive(false);
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
        mixer.audioMixer.SetFloat("masterVolume", (80 * masterSlider.value) - 80);
    }

    public void SliderMusicChanged()
    {
        mixer.audioMixer.SetFloat("musicVolume", (80 * musicSlider.value) - 80);
    }

    public void SliderMenuMusicChanged()
    {
        mixer.audioMixer.SetFloat("menuMusicVolume", (80 * menuMusicSlider.value) - 80);
    }

    public void SliderSoundsChanged()
    {
        mixer.audioMixer.SetFloat("soundsVolume", (80 * soundsSlider.value) - 80);
    }

    public void ReturnToDefaultSettings()
    {
        mixer.audioMixer.SetFloat("masterVolume", 0);
        mixer.audioMixer.SetFloat("musicVolume", 0);
        mixer.audioMixer.SetFloat("menuMusicVolume", 0);
        mixer.audioMixer.SetFloat("soundsVolume", 0);

        masterSlider.value = 1f;
        musicSlider.value = 1f;
        menuMusicSlider.value = 1f;
        soundsSlider.value = 1f;
    }

    public void CloseSettings()
    {
        SaveFieldsToStaticClass();
        gameObject.SetActive(false);
    }

    private void SaveFieldsToStaticClass()
    {
        PlayerPrefs.SetFloat("masterVolume", (80 * masterSlider.value) - 80);
        PlayerPrefs.SetFloat("musicVolume", (80 * musicSlider.value) - 80);
        PlayerPrefs.SetFloat("menuMusicVolume", (80 * menuMusicSlider.value) - 80);
        PlayerPrefs.SetFloat("soundsVolume", (80 * soundsSlider.value) - 80);
    }
}
