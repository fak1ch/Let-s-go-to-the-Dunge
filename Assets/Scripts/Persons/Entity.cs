using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Entity : MonoBehaviour
{
    protected GameObject _player;
    protected PlayerCharacteristic _playerCharacteristic;
    protected MainScript _mainScript;

    protected Animator _animator;
    protected bool _animHasBeenChanged = false;

    private IMove _moveBehaviour;

    public abstract void TakeDamage(int damage, GameObject killer);

    protected virtual void Start()
    {
        _player = StaticClass.player;
        _playerCharacteristic = StaticClass.playerCharacteristic;
        _mainScript = StaticClass.mainScript;

        TryGetComponent(out _animator);
    }

    protected void SetMoveBehaviour(IMove moveBehaviour)
    {
        _moveBehaviour = moveBehaviour;
    }

    public void Move(Vector3 target, bool playerAlive)
    {
        if (_moveBehaviour != null)
            _moveBehaviour.Move(target, playerAlive);
        else
            Debug.Log("I can't move");
    }
}
