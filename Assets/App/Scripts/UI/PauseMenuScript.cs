using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _control;

    private void Update()
    {
        if (!_pauseMenu.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                _pauseMenu.SetActive(true);
            }
        }
        else
        {
            if (!_settings.activeSelf && !_control.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ButtonResumeGame();
                }
            }
            else if (_control.activeSelf && !_settings.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _control.SetActive(false);
                }
            }
            else if (!_control.activeSelf && _settings.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _settings.SetActive(false);
                }
            }
        }
    }

    public void ButtonOpenPauseMenu()
    {
        if (!_pauseMenu.activeSelf)
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
        }
    }

    public void ButtonResumeGame()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
    }

    public void ButtonControl()
    {
        _control.SetActive(true);
    }

    public void ButtonSettings()
    {
        _settings.SetActive(true);
    }

    public void ButtonMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
