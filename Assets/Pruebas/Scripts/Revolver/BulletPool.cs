using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance; 

    [Header("Bullet Pool Settings")]
    public GameObject[] bullets; 

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        ResetBullets();
        return null;
    }

    public void ResetBullets()
    {
        foreach (GameObject bullet in bullets)
        {
            bullet.SetActive(false);
        }
    }
    // Resetear Pos
    public void ResetBulletPosition(Bullet bullet)
    {
        bullet.transform.position = Gun.Instance.firePoint.position;
        bullet.transform.rotation = Gun.Instance.firePoint.rotation;
    }
}