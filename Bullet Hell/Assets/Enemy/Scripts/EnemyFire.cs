using UnityEngine;

public class EnemyFire : MonoBehaviour {
    
    public GameObject Shot;
    public Transform BulletSpawn;
    public BulletPool bulletPool;
    public SteamVR_PlayArea PlayArea;

    [SerializeField]
    private float fireRate;

    private float nextFire;

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
