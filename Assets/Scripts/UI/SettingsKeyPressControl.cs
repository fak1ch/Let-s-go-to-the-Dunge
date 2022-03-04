using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsKeyPressControl : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _menuMusicSlider;
    [SerializeField] private Slider _soundsSlider;

    private void Start()
    {
        ApplySaveSettings();
    }

    private void ApplySaveSettings()
    {
        _mixer.audioMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("masterVolume"));
        _mixer.audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
        _mixer.audioMixer.SetFloat("menuMusicVolume", PlayerPrefs.GetFloat("menuMusicVolume"));
        _mixer.audioMixer.SetFloat("soundsVolume", PlayerPrefs.GetFloat("soundsVolume"));

        _masterSlider.value = (PlayerPrefs.GetFloat("masterVolume") + 80) / 80;
        _musicSlider.value = (PlayerPrefs.GetFloat("musicVolume") + 80) / 80;
        _menuMusicSlider.value = (PlayerPrefs.GetFloat("menuMusicVolume") + 80) / 80;
        _soundsSlider.value = (PlayerPrefs.GetFloat("soundsVolume") + 80) / 80;

        Vector3 vec = GetComponent<RectTransform>().position;
        vec.z = 0;
        GetComponent<RectTransform>().position = vec;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSettings();
        }
    }

    public void SliderMasterChanged()
    {
        _mixer.audioMixer.SetFloat("masterVolume", (80 * _masterSlider.value) - 80);
    }

    public void SliderMusicChanged()
    {
        _mixer.audioMixer.SetFloat("musicVolume", (80 * _musicSlider.value) - 80);
    }

    public void SliderMenuMusicChanged()
    {
        _mixer.audioMixer.SetFloat("menuMusicVolume", (80 * _menuMusicSlider.value) - 80);
    }

    public void SliderSoundsChanged()
    {
        _mixer.audioMixer.SetFloat("soundsVolume", (80 * _soundsSlider.value) - 80);
    }

    public void ReturnToDefaultSettings()
    {
        _mixer.audioMixer.SetFloat("masterVolume", 0);
        _mixer.audioMixer.SetFloat("musicVolume", 0);
        _mixer.audioMixer.SetFloat("menuMusicVolume", 0);
        _mixer.audioMixer.SetFloat("soundsVolume", 0);

        _masterSlider.value = 1f;
        _musicSlider.value = 1f;
        _menuMusicSlider.value = 1f;
        _soundsSlider.value = 1f;
    }

    private void CloseSettings()
    {
        SaveFieldsToStaticClass();
        gameObject.SetActive(false);
    }

    private void SaveFieldsToStaticClass()
    {
        PlayerPrefs.SetFloat("masterVolume", (80 * _masterSlider.value) - 80);
        PlayerPrefs.SetFloat("musicVolume", (80 * _musicSlider.value) - 80);
        PlayerPrefs.SetFloat("menuMusicVolume", (80 * _menuMusicSlider.value) - 80);
        PlayerPrefs.SetFloat("soundsVolume", (80 * _soundsSlider.value) - 80);
    }
}
