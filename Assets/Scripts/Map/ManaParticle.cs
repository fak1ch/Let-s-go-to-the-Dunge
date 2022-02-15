using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaParticle : MonoBehaviour
{
    private bool lockerOpen = true;
    public int manacost;

    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lockerOpen)
        {
            if (collision.CompareTag("Player"))
            {
                lockerOpen = false;
                StaticClass.weaponsInventory.weapons[StaticClass.weaponsInventory.activeGun].GetComponent<Weapon>().ChangeManaBar(manacost);
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        if (DistanceBetween2dPoints(player.transform.position, gameObject.transform.position) < 100)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, 5 * Time.deltaTime);
        }
    }

    private float DistanceBetween2dPoints(Vector2 vec1, Vector2 vec2)
    {
        float distance;

        distance = Mathf.Pow(Mathf.Pow(vec2.x - vec1.x, 2) + Mathf.Pow(vec2.y - vec1.y, 2), 0.5f);

        return distance;
    }
}
