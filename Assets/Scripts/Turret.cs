using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private const float SHOOT_DELAY = 0.1f;
    private const float BULLET_LIFETIME = 3f;

    [SerializeField] private GameObject m_target;
    [SerializeField] private GameObject m_turretPivot;
    [SerializeField] private GameObject m_turretHead;
    [SerializeField] private GameObject m_bulletSpawn;
    [SerializeField] private PrefabPool m_bulletPrefabPool;

    private void Awake()
    {
        StartCoroutine(ShootCoroutine());
    }

    private void Update()
    {
        RotateToTarget();
    }

    public void Shoot()
    {
        Bullet bullet = m_bulletPrefabPool.GetPrefab<Bullet>();
        bullet.transform.position = m_bulletSpawn.transform.position;
        Vector3 shootDirection = m_turretPivot.transform.forward;
        bullet.Launch(shootDirection);
        StartCoroutine(BulletLifetimeCoroutine(bullet));
    }

    private void RotatePivotYAxisToTarget()
    {
        Vector3 targetDirection = m_target.transform.position - m_turretPivot.transform.position;
        targetDirection.y = 0f;
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            m_turretPivot.transform.rotation = Quaternion.Slerp(m_turretPivot.transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    private void RotateToTarget()
    {
        if (m_target != null)
        {
            RotatePivotYAxisToTarget();
        }
    }

    private IEnumerator BulletLifetimeCoroutine(Bullet bullet)
    {
        yield return new WaitForSeconds(BULLET_LIFETIME);
        bullet.StopLaunch();
        m_bulletPrefabPool.SimulateDestroy(bullet.gameObject);
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(SHOOT_DELAY);
        }
    }
}
