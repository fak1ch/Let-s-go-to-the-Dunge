using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManaAfterDeath : MonoBehaviour
{
    public GameObject manaParticle;

    public void DropManaAfterDead()
    {
        if (Random.Range(0,5) == 0)
        {
            int i = Random.Range(1, 4);

            Vector2 vec = transform.position;
            for (int k = 0; k < i; k++)
            {
                vec.x += Random.Range(-50, 51);
                vec.y += Random.Range(-50, 51);
                Instantiate(manaParticle, vec, Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }
}
