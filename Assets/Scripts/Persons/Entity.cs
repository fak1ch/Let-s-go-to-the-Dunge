using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Entity : MonoBehaviour
{
    protected Animator _animator;
    protected bool _animHasBeenChanged = false;

    private IMove _moveBehaviour;
    private IRotate _rotateBehaviour;

    public abstract void TakeDamage(int damage, GameObject killer);

    protected virtual void Start()
    {
        TryGetComponent(out _animator);
    }

    protected void SetMoveBehaviour(IMove moveBehaviour)
    {
        _moveBehaviour = moveBehaviour;
    }

    protected void SetRotateBehaviour(IRotate rotateBehaviour)
    {
        _rotateBehaviour = rotateBehaviour;
    }

    protected void Move(Vector3 target, bool playerAlive)
    {
        if (_moveBehaviour != null)
            _moveBehaviour.Move(target, playerAlive);
        else
            Debug.Log("I can't move");
    }

    protected float Rotate(Vector3 target, Transform obj)
    {
        if (_rotateBehaviour != null)
            return _rotateBehaviour.Rotate(target, obj);
        else
            Debug.Log("I can't rotate");

        return 0;
    }
}
