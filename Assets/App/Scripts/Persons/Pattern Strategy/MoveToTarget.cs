using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : IMove
{
    private Vector3 _startPosition;
    private Vector3 position;
    private Transform _transform;
    private float _speed;


    public MoveToTarget(Transform transform, float speed, Vector3 startPosition)
    {
        _transform = transform;
        _startPosition = startPosition;
        _speed = speed;
    }

    public void Move(Vector3 target, bool playerAlive)
    {
        if (playerAlive)
            _transform.position = Vector3.MoveTowards(_transform.position, target, _speed * Time.fixedDeltaTime);
        else
            MoveToStartPosition();
    }

    private void MoveToStartPosition()
    {
        if (position != _startPosition)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, _startPosition, _speed * Time.fixedDeltaTime);
        }
    }
}
