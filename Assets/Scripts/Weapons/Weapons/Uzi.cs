using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uzi : Weapon, IWeapon
{
    void Start()
    {
        StartMethod(GetComponent<Uzi>());
    }

    private void Update()
    {
        RotateWeapon();
        AttackFromWeapon(); 
    }
}
