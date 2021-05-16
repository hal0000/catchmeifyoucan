using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    SwerveInputSystem _swerveInputSystem;
    GameManager _gm;

    void Awake()
    {
        _gm = GameManager.Instance;
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
    }

    void FixedUpdate()
    {
        transform.Translate(_gm.PlayerSpeed * -0.01f, 0, Time.fixedDeltaTime * _gm.PlayerHorizontalSpeed * _swerveInputSystem.MoveFactorX);
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -2f, 2f));
    }
}