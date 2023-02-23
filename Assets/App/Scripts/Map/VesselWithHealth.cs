using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselWithHealth : MonoBehaviour
{
    [SerializeField] private int _humberOfHits;
    [SerializeField] private bool _mostPercent = false;
    [SerializeField] private GameObject _healHealth;

    private bool _isOpen = true;

    public void SpawnHealth()
    {
        if (_mostPercent)
        {
            if (Random.Range(0,2) == 0)
            {
                Instantiate(_healHealth, gameObject.transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (Random.Range(0, 4) == 0)
            {
                Instantiate(_healHealth, gameObject.transform.position, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet") && _isOpen)
        {
            _isOpen = false;
            if (_humberOfHits == 1)
            {
                _humberOfHits -= 1;
                SpawnHealth();
            }
            else if (_humberOfHits > 1)
            {
                _isOpen = true;
                _humberOfHits -= 1;
                Destroy(collision.gameObject);
            }
        }
    }
}
