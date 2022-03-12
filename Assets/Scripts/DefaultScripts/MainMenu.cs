using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private enum TypeOfDevice { PC, Phone }
    [SerializeField] private TypeOfDevice _typeOfDevice;
    [SerializeField] private GameObject _settingsMenu;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        StaticClass.typeOfDevice = (StaticClass.TypeOfDevice)_typeOfDevice;
    }

    public void SetActiveSettings()
    {
        _settingsMenu.SetActive(true);
    }
}
