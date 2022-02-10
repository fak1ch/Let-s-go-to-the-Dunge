using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadPistol : Weapon, IWeapon
{
    void Start()
    {
        StartMethod(GetComponent<BadPistol>());
    }

    private void Update()
    {
        RotateWeapon();
        AttackFromWeapon();
    }
}
