using UnityEngine;

public class RunStateHamster : State
{
    private GameObject _target;
    private Entity _thisEntity;
    private BossHamsterAnimationController _animationController;

    public RunStateHamster(Entity entity, GameObject target, BossHamsterAnimationController animationController)
    {
        _thisEntity = entity;
        _target = target;
        _animationController = animationController;
    }

    public override void Enter()
    {
        _animationController.StateRun();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        _thisEntity.Move(_target.transform.position, _target.activeInHierarchy);
    }
}