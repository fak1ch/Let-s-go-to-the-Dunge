using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManaAndAmethistsAfterDeath : MonoBehaviour
{
    [SerializeField] private GameObject _manaParticle;
    [SerializeField] private GameObject _amethyst;

    public void DropManaAndAmethystAfterDead()
    {
        if (Random.Range(0,5) == 0)
        {
            int i = Random.Range(1, 4);

            for (int k = 0; k < i; k++)
            {
                Instantiate(_manaParticle, GetRandomSpawnPoint(-50,50), Quaternion.identity);
            }
        }

        if (Random.Range(0, 5) == 0)
        {
            int i = Random.Range(1, 4);

            for (int k = 0; k < i; k++)
            {
                Instantiate(_amethyst, GetRandomSpawnPoint(-70, 70), Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }

    private Vector2 GetRandomSpawnPoint(int from, int before)
    {
        Vector2 vec = transform.position;
        vec.x += Random.Range(from, before);
        vec.y += Random.Range(from, before);

        return vec;
    }
}
