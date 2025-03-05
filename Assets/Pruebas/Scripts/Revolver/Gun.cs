using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public static Gun Instance; 

    [Header("Gun Settings")]
    public Transform firePoint;
    public int maxAmmo = 30;
    public int currentAmmo;
    public float reloadTime = 2f;

    private bool isReloading = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        Prepare();
    }

    public void Prepare()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    private void Update()
    {
        if (currentAmmo == 0)
            Reload();
    }

    public void Fire()
    {
        if (isReloading || currentAmmo <= 0) return;

        GameObject bullet = BulletPool.Instance.GetBullet();
        if (bullet != null)
        {
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            bullet.SetActive(true);

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetDirection(firePoint.forward);
            }
        }

        currentAmmo--;

    }

    public void Reload()
    {
        if (isReloading) return;
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
}