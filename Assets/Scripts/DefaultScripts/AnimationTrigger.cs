using DungeonHeroes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _playerController;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (_playerController.MoveInput.x !=0 || _playerController.MoveInput.y != 0)
        {
            _animator.SetBool("isRunning", true);
        }
        else if (_playerController.MoveInput.x == 0 && _playerController.MoveInput.y == 0)
        {
            _animator.SetBool("isRunning", false);
        }
    }
}
