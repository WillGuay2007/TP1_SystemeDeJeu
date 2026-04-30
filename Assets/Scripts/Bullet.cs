using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
        }
    }

    //TODO: Make it so it actually collide. I think its because of rigid body
    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
    }

    private IEnumerator StartBulletLaunch(Vector3 direction)
    {
        while (true)
        {
            MoveBullet(direction);
            yield return null;
        }
    }

    private void MoveBullet(Vector3 direction)
    {
        transform.Translate(direction * BULLET_VELOCITY_PER_SECOND * Time.deltaTime);
    }
}
