using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDestroyObj : MonoBehaviour
{
    public GameObject badPistol;
    public MoveHeroe mh;
    void Start()
    {
        StartCoroutine(PickBadPistol());
    }

    private IEnumerator PickBadPistol()
    {
        var pistol = Instantiate(badPistol, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        mh.PickBadPistol(pistol);
        Destroy(gameObject);
    }
}
