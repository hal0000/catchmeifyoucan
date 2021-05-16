using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform _target;
    public float Speed = 30f;
    public GameObject ImpactEffect;
    public delegate void DestroyAction();
    public static event DestroyAction OnEnemyDestroy;
    public void Seek(Transform target) => _target = target;
    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = _target.position - transform.position;
        float distanceThisFrame = Speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
    void HitTarget()
    {
        GameObject effect1 = Instantiate(ImpactEffect, transform.position, transform.rotation);
        Destroy(effect1, 2.2f);
        Destroy(gameObject);
        Destroy(_target.gameObject);
        OnEnemyDestroy();
    }
}
