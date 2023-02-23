using System;

[Serializable]
public class SettingsGameData
{
    private float _masterSlider;
    private float _musicSlider;
    private float _menuMusicSlider;
    private float _soundsSlider;

    public float MasterSlider { get => _masterSlider; set => _masterSlider = value; }
    public float MusicSlider { get => _musicSlider; set => _musicSlider = value; }
    public float MenuMusicSlider { get => _menuMusicSlider; set => _menuMusicSlider = value; }
    public float SoundsSlider { get => _soundsSlider; set => _soundsSlider = value; }

    public SettingsGameData()
    {
        MasterSlider = 1;
        MusicSlider = 1;
        MenuMusicSlider = 1;
        SoundsSlider = 1;
    }
}
