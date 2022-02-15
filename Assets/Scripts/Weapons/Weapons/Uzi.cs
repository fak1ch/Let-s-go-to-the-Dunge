using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uzi : Weapon, IWeapon
{
    void Start()
    {
        StartMethod();
    }

    private void Update()
    {
        RotateWeapon();
        AttackFromWeapon(); 
    }
}
