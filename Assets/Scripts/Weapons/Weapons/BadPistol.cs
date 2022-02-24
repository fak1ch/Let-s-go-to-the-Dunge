using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadPistol : Weapon, IWeapon
{
    void Start()
    {
        StartMethod();
    }

    private void Update()
    {
        UpdateMethod();
    }

    private void FixedUpdate()
    {
        FixedUpdateMethod();
    }
}
