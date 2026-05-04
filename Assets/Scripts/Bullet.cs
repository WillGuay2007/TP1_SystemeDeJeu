using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rigidBody;
    private Coroutine m_launchCoroutine;
    private const float BULLET_VELOCITY_PER_SECOND = 25f;
    public void Launch(Vector3 direction)
    {
        m_launchCoroutine = StartCoroutine(StartBulletLaunch(direction));
    }

    public void StopLaunch()
    {
        if (m_launchCoroutine != null)
        {
            StopCoroutine(m_launchCoroutine);
            m_launchCoroutine = null;
            m_rigidBody.linearVelocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle") StopLaunch();
    }

    private IEnumerator StartBulletLaunch(Vector3 direction)
    {
        while (true)
        {
            MoveBullet(direction);
            yield return new WaitForFixedUpdate();
        }
    }

    private void MoveBullet(Vector3 direction)
    {
        m_rigidBody.linearVelocity = direction * BULLET_VELOCITY_PER_SECOND;
    }
}
