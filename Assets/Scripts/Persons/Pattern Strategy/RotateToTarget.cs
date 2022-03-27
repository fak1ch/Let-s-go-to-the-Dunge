using UnityEngine;

public class RotateToTarget
{
    private Camera _camera;

    public RotateToTarget(Camera camera)
    {
        _camera = camera;
    }

    public float Rotate(Vector3 target, Transform obj)
    {
        Vector3 vec = _camera.WorldToScreenPoint(target);
        Vector3 objectPos = _camera.WorldToScreenPoint(obj.position);
        vec.x = vec.x - objectPos.x;
        vec.y = vec.y - objectPos.y;

        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;

        obj.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        return angle;
    }
}
