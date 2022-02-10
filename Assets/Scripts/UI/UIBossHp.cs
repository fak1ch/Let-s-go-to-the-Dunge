using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBossHp : MonoBehaviour
{
    private GameObject image;
    public float health = 0;
    private float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        image = transform.GetChild(0).gameObject;
        image.GetComponent<Image>().enabled = false;
        GetComponent<Image>().enabled = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        image.GetComponent<Image>().fillAmount = health / maxHealth;
    }

    public void StartHealth(int health)
    {
        maxHealth = health;
        this.health = maxHealth;
        image.GetComponent<Image>().fillAmount = 1;
        image.GetComponent<Image>().enabled = true;
        GetComponent<Image>().enabled = true;
    }
}
