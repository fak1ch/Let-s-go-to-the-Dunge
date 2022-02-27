using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenZone : MonoBehaviour
{
    [SerializeField] private Collider2D _collider2d;
    private Collider2D _playerCollider;
    private bool isOpen = true;
    private void FixedUpdate()
    {
        if (!isOpen)
        {
            if (_collider2d !=null)
            {
                if (!_playerCollider.IsTouching(_collider2d))
                {
                    isOpen = true;
                    StaticClass.weaponsInventory.AllGunsGreenZoneState(false);
                }
            }
            else
            {
                StaticClass.weaponsInventory.AllGunsGreenZoneState(false);
                Destroy(this);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isOpen)
            {
                if (_playerCollider == null)
                _playerCollider = collision;
                isOpen = false;
                StaticClass.weaponsInventory.AllGunsGreenZoneState(true);
            }
        }
    }
}
