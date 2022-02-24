using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazuka : Weapon, IWeapon
{
    void Start() 
    {
        StartMethod();
    }

    private void Update()
    {
        RotateWeapon();
        ClickButtonAttack();
    }
}
