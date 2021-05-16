using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform target;
    [Header("Specs.")]
    public float Range = 10f;
    public float FireRate = 0.0025f;
    private float FireCountDown = 0f;

    [Header("Setup Fields")]
    public Transform PartToRotate;
    public float turnSpeed = 15f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject fireLight;

    void Start() => InvokeRepeating("UpdateTarget", 0f, 0.5f);

    
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= Range)
            target = nearestEnemy.transform;
        else
            target = null;
    }

    void Update()
    {
        FireCountDown -= Time.deltaTime * 25;
        if (target == null)
            return;
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);//use look rotation if object has some legit mesh
        if (FireCountDown <= 0)
        {
            Shoot();
            FireCountDown = 1f / FireRate;
        }
    }
    void Shoot()
    {
        GameObject bulletGO1 = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO1.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Seek(target);
        GameObject effect = Instantiate(fireLight, firePoint.position, firePoint.rotation);
        Destroy(effect, 0.05f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}