using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazuka : Weapon, IWeapon
{
    void Start()
    {
        StartMethod(GetComponent<Bazuka>());
    }

    private void Update()
    {
        RotateWeapon();
        AttackFromWeapon();
    }
}
