using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointForRotateAroundScript : MonoBehaviour
{
    [SerializeField] private int _angle;
    public Transform Target;
    private Vector3 _vec;

    void Update()
    {
        transform.position = Target.TransformPoint(100, 100, 1);
        transform.rotation = Target.rotation;
    }
}
