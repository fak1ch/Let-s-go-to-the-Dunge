using UnityEngine;

public class BossHamsterAnimationController
{
    private Animator _animator;

    public BossHamsterAnimationController(Animator animator)
    {
        _animator = animator;
    }

    public void StateRun()
    {
        _animator.SetBool("isRun", true);
        _animator.SetBool("isShoot", false);
    }

    public void StateShoot()
    {
        _animator.SetBool("isRun", false);
        _animator.SetBool("isShoot", true);
    }

    public void StateIdle()
    {
        _animator.SetBool("isRun", false);
        _animator.SetBool("isShoot", false);
    }
}
