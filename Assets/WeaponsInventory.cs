using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsInventory : MonoBehaviour
{
    public GameObject gunPlace;
    public bool androidClickAction = false;
    public bool allowPick = true;
    public int activeGun = 0;
    public List<GameObject> weapons = new List<GameObject>();

    private bool allowDrop = true;
    private float timerTime = 0.5f;
    private WeaponsPanel weaponsPanel;

    void Start()
    {
        weaponsPanel = GameObject.Find("WeaponsPanel").GetComponent<WeaponsPanel>();
    }

    void FixedUpdate()
    {
        if (weapons.Count >= 1)
        {
            DropGun();
            ChangeGun();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            if (allowPick)
            {
                if (Input.GetKey(KeyCode.E) || androidClickAction)
                {
                    PickGun(collision.gameObject);
                }
            }
        }
    }

    public void PickGun(GameObject weapon)
    {
        if (weapons.Count == 2)
        {
            weapons[activeGun].transform.SetParent(default);
            weapons[activeGun].GetComponent<Weapon>().IsDropped = true;
            weapons.RemoveAt(activeGun);
            weapons.Add(weapon);
            weapon.gameObject.transform.position = gunPlace.transform.position;
            weapon.gameObject.transform.SetParent(gunPlace.transform);
            weapon.gameObject.GetComponent<Weapon>().IsDropped = false;
            SelectGun(weapons.Count);
            weapons[activeGun].GetComponent<SpriteRenderer>().sortingOrder = 3;
            StartCoroutine(AllowPickCorutine());
        }
        else if (weapons.Count == 1 || weapons.Count == 0)
        {
            weapons.Add(weapon.gameObject);
            weapon.gameObject.transform.position = gunPlace.transform.position;
            weapon.gameObject.transform.SetParent(gunPlace.transform);
            weapon.gameObject.GetComponent<Weapon>().IsDropped = false;
            SelectGun(weapons.Count);
            weapons[activeGun].GetComponent<SpriteRenderer>().sortingOrder = 3;
            StartCoroutine(AllowPickCorutine());
        }

        weaponsPanel.ChangeSprite(weapons[activeGun].GetComponent<SpriteRenderer>().sprite);
    }

    private void DropGun()
    {
        if (allowDrop && StaticClass.typeOfDevice == StaticClass.TypeOfDevice.PC)
        {
            if (!weapons[activeGun].GetComponent<Weapon>().IsDropped && Input.GetKey(KeyCode.Q))
            {
                if (weapons.Count == 2)
                {
                    weapons[activeGun].GetComponent<SpriteRenderer>().sortingOrder = 1;
                    weapons[activeGun].transform.SetParent(default);
                    weapons[activeGun].GetComponent<Weapon>().IsDropped = true;
                    weapons.RemoveAt(activeGun);
                    SelectGun(weapons.Count);
                    StartCoroutine(AllowDropCorutine());
                    weaponsPanel.ChangeSprite(weapons[activeGun].GetComponent<SpriteRenderer>().sprite);
                }
                else if (weapons.Count == 1) 
                {
                    weapons[activeGun].GetComponent<SpriteRenderer>().sortingOrder = 1;
                    weapons[activeGun].transform.SetParent(default);
                    weapons[activeGun].GetComponent<Weapon>().IsDropped = true;
                    weapons.RemoveAt(activeGun);

                    activeGun = -1;
                    StartCoroutine(AllowDropCorutine());
                    weaponsPanel.ChangeSprite(null);
                }
            }
        }
    }

    public void SelectGun(int i)
    {
        if (i == 1)
        {
            if (weapons.Count == 2)
            {
                activeGun = 0;
                weapons[0].SetActive(true);
                weapons[1].SetActive(false);
            }
            else
            {
                activeGun = 0;
                weapons[0].SetActive(true);
            }
        }
        if (i == 2)
        {
            activeGun = 1;
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
        }
    }

    private void ChangeGun()
    {
        if (weapons.Count == 2)
        {
            if (Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Alpha1))
            {
                activeGun = 0;
                weapons[0].SetActive(true);
                weapons[1].SetActive(false);
            }
            if (Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Alpha2))
            {
                activeGun = 1;
                weapons[0].SetActive(false);
                weapons[1].SetActive(true);
            }
            weaponsPanel.ChangeSprite(weapons[activeGun].GetComponent<SpriteRenderer>().sprite);
        }
    }

    public void PickBadPistol(GameObject pistol)
    {
        weapons.Add(pistol);
        pistol.transform.position = gunPlace.transform.position;
        pistol.transform.SetParent(gunPlace.transform);
        weapons[0].GetComponent<SpriteRenderer>().sortingOrder = 3;
        pistol.GetComponent<Weapon>().IsDropped = false;
        activeGun = 0;
    }

    private IEnumerator AllowDropCorutine()
    {
        allowDrop = false;
        yield return new WaitForSeconds(timerTime);
        allowDrop = true;
    }

    private IEnumerator AllowPickCorutine()
    {
        allowPick = false;
        yield return new WaitForSeconds(timerTime);
        allowPick = true;
    }
}
