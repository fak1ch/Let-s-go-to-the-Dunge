using UnityEngine;
using UnityEngine.AI;

public interface IMove
{
    public void Move(Vector3 target, bool playerAlive);
}
