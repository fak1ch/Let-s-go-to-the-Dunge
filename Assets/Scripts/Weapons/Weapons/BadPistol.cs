using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadPistol : Weapon
{
    void Start()
    {
        IsDropped = false;
        StartMethod();
    }

    private void Update()
    {
        UpdateMethod();
    }
}
