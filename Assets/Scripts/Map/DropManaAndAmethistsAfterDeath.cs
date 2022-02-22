using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManaAndAmethistsAfterDeath : MonoBehaviour
{
    [SerializeField]private GameObject _manaParticle;
    [SerializeField]private GameObject _amethyst;

    public void DropManaAndAmethystAfterDead()
    {
        if (Random.Range(0,5) == 0)
        {
            int i = Random.Range(1, 4);

            Vector2 vec = transform.position;
            for (int k = 0; k < i; k++)
            {
                vec.x += Random.Range(-50, 51);
                vec.y += Random.Range(-50, 51);
                Instantiate(_manaParticle, vec, Quaternion.identity);
            }
        }

        if (Random.Range(0, 5) == 0)
        {
            int i = Random.Range(1, 4);

            Vector2 vec = transform.position;
            for (int k = 0; k < i; k++)
            {
                vec.x += Random.Range(-70, 71);
                vec.y += Random.Range(-70, 71);
                Instantiate(_amethyst, vec, Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }
}
