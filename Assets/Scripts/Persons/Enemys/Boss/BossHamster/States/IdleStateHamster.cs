using UnityEngine;

public class IdleStateHamster : State
{
    private BossHamsterAnimationController _animationController;

    public IdleStateHamster(BossHamsterAnimationController animationController)
    {
        _animationController = animationController;
    }

    public override void Enter()
    {
        _animationController.StateIdle();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
