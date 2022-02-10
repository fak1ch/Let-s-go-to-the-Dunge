using System;
using UnityEngine;

public static partial class StaticClass
{
    public enum TypeOfDevice { PC, Phone }
    public static TypeOfDevice typeOfDevice = TypeOfDevice.Phone;

    public static GameObject player;
    public static MoveHeroe moveHeroe;
    public static MainScript mainScript;
}