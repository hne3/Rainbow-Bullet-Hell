using UnityEngine;

public class EnemyFire : MonoBehaviour {
    
    public Transform BulletSpawn;
    public BulletPool bulletPool;
    public float fireRate;

    private float nextFire = 0.0f;

    private void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Bullet bullet = bulletPool.GetBullet();

            if (bullet)
            {
                bullet.transform.position = BulletSpawn.position;
                bullet.transform.rotation = BulletSpawn.rotation;
                bullet.gameObject.SetActive(true);
            }
        }
    }
}
