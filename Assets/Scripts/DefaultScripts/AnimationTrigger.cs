using Assets.Scripts;
using DungeonHeroes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    private Animator animator;
    private IScript pc;
    void Start()
    {
        pc = GetComponent<PlayerController>();
        if (pc == null)
        {
            pc = GetComponent<StartScene>();
        }

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.MoveInput.x !=0 || pc.MoveInput.y != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else if (pc.MoveInput.x == 0 && pc.MoveInput.y == 0)
        {
            animator.SetBool("isRunning", false);
        }
    }
}
