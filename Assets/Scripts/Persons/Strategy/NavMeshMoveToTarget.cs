using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMoveToTarget : IMove
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private bool _animHasBeenChanged = false;
    private Vector3 _startPosition;
    private Vector3 position;


    public NavMeshMoveToTarget(NavMeshAgent navMeshAgent, Animator animator, Vector3 startPosition)
    {
        _navMeshAgent = navMeshAgent;
        _animator = animator;
        _startPosition = startPosition;
    }

    public void Move(Vector3 target, bool playerAlive)
    {
        if (playerAlive)
        {
            _navMeshAgent.SetDestination(target);
            if (_animator != null)
            {
                if (!_animHasBeenChanged)
                {
                    _animHasBeenChanged = !_animHasBeenChanged;
                    _animator.SetBool("isRun", true);
                }
            }
        }
        else
        {
            MoveToStartPosition();
        }
    }

    private void MoveToStartPosition()
    {
        if (position != _startPosition)
        {
            _navMeshAgent.SetDestination(_startPosition);
        }
        else
        {
            if (_animator != null)
            {
                if (_animHasBeenChanged)
                {
                    _animHasBeenChanged = !_animHasBeenChanged;
                    _animator.SetBool("isRun", false);
                }
            }
        }
    }
}
