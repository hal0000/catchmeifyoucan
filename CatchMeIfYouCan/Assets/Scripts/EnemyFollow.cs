using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    Vector3 _target;
    Rigidbody _rb;
    GameManager _gm;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _gm = GameManager.Instance;
    }

    void FixedUpdate() => _rb.MovePosition(Vector3.MoveTowards(transform.position, _gm.Player.transform.position, _gm.EnemySpeed * Time.fixedDeltaTime));
}