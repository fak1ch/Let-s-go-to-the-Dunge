using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private int health;
    public int numberOfLives;
    public Image[] lives;
    public Sprite fullLive;
    public Sprite halfLive;
    public Sprite emptyLive;

    private MoveHeroe moveHeroe;
    void Start()
    {
        moveHeroe = GameObject.FindWithTag("Player").GetComponent<MoveHeroe>();
        health = moveHeroe.health;
    }

    // Update is called once per frame
    void Update()
    {
        health = moveHeroe.health;
        if (health > numberOfLives)
        {
            moveHeroe.health = numberOfLives;
        }
        for(int i = 0; i < 2 * lives.Length; i++)
        {
            int k = i % 2;
            double j = i / 2;
            Math.Floor(j);
            if (i < health)
            {
                lives[(int)j].sprite = fullLive;
            }
            if (i == health - 1 && k==0)
            {
                lives[(int)j].sprite = halfLive;
            }
            if (i > health)
            {
                lives[(int)j].sprite = emptyLive;
            }

            if (i < numberOfLives)
            {
                lives[(int)j].enabled = true;
            }
            else
            {
                lives[(int)j].enabled = false;
            }
        }
    }
}
