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

    private Storage _storage;
    private SettingsGameData _gameData;

    private void Start()
    {
        _storage = new Storage();
        _gameData = (SettingsGameData)_storage.Load(new SettingsGameData());

        LoadSettings();
    }

    private void LoadSettings()
    {
        _masterSlider.value = _gameData.MasterSlider;
        _musicSlider.value = _gameData.MusicSlider;
        _menuMusicSlider.value = _gameData.MenuMusicSlider;
        _soundsSlider.value = _gameData.SoundsSlider;

        _mixer.audioMixer.SetFloat("masterVolume", (80 * _masterSlider.value) - 80);
        _mixer.audioMixer.SetFloat("musicVolume", (80 * _musicSlider.value) - 80);
        _mixer.audioMixer.SetFloat("menuMusicVolume", (80 * _menuMusicSlider.value) - 80);
        _mixer.audioMixer.SetFloat("soundsVolume", (80 * _soundsSlider.value) - 80);

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

    public void CloseSettings()
    {
        SaveSettings();
        gameObject.SetActive(false);
    }

    private void SaveSettings()
    {
        _gameData.MasterSlider = _masterSlider.value;
        _gameData.MusicSlider = _musicSlider.value;
        _gameData.MenuMusicSlider = _menuMusicSlider.value;
        _gameData.SoundsSlider = _soundsSlider.value;

        _storage.Save(_gameData);
    }
}
