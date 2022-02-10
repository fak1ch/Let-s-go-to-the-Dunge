using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public enum TypeOfDevice { PC, Phone }
    public TypeOfDevice typeOfDevice;

    public GameObject settingsMenu;

    private void Start()
    {
        StaticClass.typeOfDevice = (StaticClass.TypeOfDevice)typeOfDevice;
    }

    public void SetActiveSettings()
    {
        settingsMenu.SetActive(true);
    }
}
