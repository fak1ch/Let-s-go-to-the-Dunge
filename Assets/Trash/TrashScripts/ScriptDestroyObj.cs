using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDestroyObj : MonoBehaviour
{
    public GameObject badPistol;
    void Start()
    {
        StartCoroutine(PickBadPistol());
    }

    private IEnumerator PickBadPistol()
    {
        var pistol = Instantiate(badPistol, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        StaticClass.weaponsInventory.PickBadPistol(pistol);
        Destroy(gameObject);
    }
}
