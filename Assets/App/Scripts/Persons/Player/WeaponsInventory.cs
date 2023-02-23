using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsInventory : MonoBehaviour
{
    public event Action<Sprite> OnWeaponChange;

    [SerializeField] private GameObject _gunPlace;
    [SerializeField] private List<GameObject> _weapons = new List<GameObject>();
    [SerializeField] private List<Weapon> _weaponsScripts = new List<Weapon>();

    private bool _allowPick = true;
    private int _activeGun = 0;
    private bool _allowDrop = true;
    private float _timer = 0.5f;

    public bool AndroidClickAction { get; set; } = false;
    public int WeaponsCount => _weapons.Count;
    public int ActiveGun => _activeGun;

    void FixedUpdate()
    {
        if (_weapons.Count >= 1)
        {
            DropGun();
            ChangeGun();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            if (Input.GetKey(KeyCode.E) || AndroidClickAction)
            {
                if (_allowPick && collision.gameObject.GetComponent<Weapon>().IsDropped)
                {
                    PickGun(collision.gameObject);
                }
            }
        }
    }

    public void PickGun(GameObject weapon)
    {
        if (_weapons.Count == 2)
        {
            _weapons[_activeGun].GetComponent<Weapon>().DropWeapon();
            _weapons.RemoveAt(_activeGun);
            _weaponsScripts.RemoveAt(_activeGun);
        }

        _weapons.Add(weapon);
        _weaponsScripts.Add(weapon.GetComponent<Weapon>());
        _weaponsScripts[_weaponsScripts.Count - 1].TakeWeapon(_gunPlace);

        SelectGun(_weapons.Count);
        StartCoroutine(AllowPickCorutine(_timer));

        OnWeaponChange?.Invoke(_weapons[_activeGun].GetComponent<SpriteRenderer>().sprite);
    }

    private void DropGun()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (!_weaponsScripts[_activeGun].IsDropped && _allowDrop)
            {
                _weapons[_activeGun].GetComponent<Weapon>().DropWeapon();
                _weapons.RemoveAt(_activeGun);
                _weaponsScripts.RemoveAt(_activeGun);
                SelectGun(_weapons.Count);
                StartCoroutine(AllowDropCorutine(_timer));
                if(_weapons.Count == 1)
                    OnWeaponChange?.Invoke(_weapons[_activeGun].GetComponent<SpriteRenderer>().sprite);
                else
                    OnWeaponChange?.Invoke(null);
            }
        }
    }

    public void SelectGun(int i)
    {
        if (i == 0)
        {
            _activeGun = -1;
        }
        if (i == 1)
        {
            if (_weapons.Count == 2)
            {
                _activeGun = 0;
                _weapons[0].SetActive(true);
                _weapons[1].SetActive(false);
            }
            else
            {
                _activeGun = 0;
                _weapons[0].SetActive(true);
            }
        }
        if (i == 2)
        {
            _activeGun = 1;
            _weapons[0].SetActive(false);
            _weapons[1].SetActive(true);
        }
    }

    private void ChangeGun()
    {
        if (_weapons.Count == 2)
        {
            if (Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Alpha1))
            {
                _activeGun = 0;
                _weapons[0].SetActive(true);
                _weapons[1].SetActive(false);
            }
            if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Alpha2))
            {
                _activeGun = 1;
                _weapons[0].SetActive(false);
                _weapons[1].SetActive(true);
            }
            OnWeaponChange?.Invoke(_weapons[_activeGun].GetComponent<SpriteRenderer>().sprite);
        }
    }

    public void AllowDropStartCorutine(float timer)
    {
        StartCoroutine(AllowDropCorutine(timer));
    }

    public void AllowPiclStartCorutine(float timer)
    {
        StartCoroutine(AllowPickCorutine(timer));
    }

    private IEnumerator AllowDropCorutine(float timer)
    {
        _allowDrop = false;
        yield return new WaitForSeconds(timer);
        _allowDrop = true;
    }

    private IEnumerator AllowPickCorutine(float timer)
    {
        _allowPick = false;
        yield return new WaitForSeconds(timer);
        _allowPick = true;
    }

    public void AllGunsGreenZoneState(bool value)
    {
        foreach(var script in _weaponsScripts)
        {
            script.GreenZone = value;
        }
    }
}
