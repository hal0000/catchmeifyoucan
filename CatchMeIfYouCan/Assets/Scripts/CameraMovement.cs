using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Target;
    public float SmoothTime = 0.3f;
    public Vector3 Offset;
    Vector3 _velocity = Vector3.zero;
    void FixedUpdate() => transform.position = Vector3.SmoothDamp(transform.position, new Vector3(Target.position.x + Offset.x, 0 + Offset.y, Offset.z), ref _velocity, SmoothTime);
}