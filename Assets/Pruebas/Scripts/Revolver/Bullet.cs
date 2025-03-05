using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 20f;
    public float lifeTime = 1f;
    public int damage = 10;
    public float impactForce = 500f;
    private Vector3 direction;

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
        transform.forward = direction;
        Invoke("DisableBullet", lifeTime);
    }

    void Update()
    {
        if (direction == Vector3.zero) return;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        DisableBullet();
    }

    void DisableBullet()
    {
        gameObject.SetActive(false);
        BulletPool.Instance.ResetBulletPosition(this);
    }
}