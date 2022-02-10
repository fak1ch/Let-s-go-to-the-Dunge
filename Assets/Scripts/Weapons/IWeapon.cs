using System;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void ChangeManaBar(int manac);
    public bool IsDropped { get; set; }
}
