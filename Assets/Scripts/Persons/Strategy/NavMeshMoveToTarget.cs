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
            ChangeAnimation(_animHasBeenChanged);
        }
        else
        {
            MoveToStartPosition();
            ChangeAnimation(_animHasBeenChanged);
        }
    }

    private void MoveToStartPosition()
    {
        _navMeshAgent.SetDestination(_startPosition);
        ChangeAnimation(_animHasBeenChanged);
    }

    private void ChangeAnimation(bool animHasBeenChanged)
    {
        if (_animator != null)
        {
            if (!_animHasBeenChanged)
            {
                _animator.SetBool("isRun", true);
            }
            else
            {
                _animator.SetBool("isRun", false);
            }

            _animHasBeenChanged = !_animHasBeenChanged;
        }
    }
}
