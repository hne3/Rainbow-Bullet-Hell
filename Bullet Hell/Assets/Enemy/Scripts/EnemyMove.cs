using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

    public Vector3 Rotation = new Vector3(0, 200, 300);
    public Vector3 Offset = new Vector3(0.1f, 0.1f, 0.1f);
    public float Speed = 2.0f;
    public float TimeBetweenPatterns = 0.3f;
    public EnemyFire[] SpawnPoints;
    public GameObject Player;
    public BulletPool bulletPool;

    private int maxArms;
    private int numArms;
    private float armFireRate = 0.1f;

    private void Start()
    {
        maxArms = SpawnPoints.Length;
        InvokeRepeating("GeneratePatternSettings", 0.0f, TimeBetweenPatterns);
    }

	private void Update ()
    {
        transform.Rotate(Rotation * Time.deltaTime * Speed);
    }

    private void GeneratePatternSettings()
    {
        foreach (EnemyFire e in SpawnPoints)
        {
            e.gameObject.SetActive(false);
        }

        armFireRate = Random.Range(0.07f, 0.1f);
        numArms = Random.Range(1, maxArms);
        for (int i = 0; i < numArms; i++)
        {
            int index = Random.Range(0, numArms);
            if (!SpawnPoints[index].gameObject.activeSelf)
            {
                SpawnPoints[index].fireRate = armFireRate;
                SpawnPoints[index].gameObject.SetActive(true);
                if (i == 0)
                {
                    SpawnPoints[i].transform.LookAt(Player.transform, Vector3.up);
                }
            }
            else
            {
                i--;
            }
        }
        Rotation.y = Random.Range(45, 360);
        Rotation.z = Random.Range(45, 360);
        Speed = Random.Range(1, 2);
    }

    private void GenerateSphereAttack()
    {
        Mesh m = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = m.vertices;
        Vector3[] normals = m.normals;
        for (int i = 0; i < vertices.Length; i++)
        {
            if (i % 5 == 0)
            {
                Bullet bullet = bulletPool.GetBullet();

                if (bullet)
                {
                    bullet.transform.position = transform.TransformPoint(vertices[i]) + Offset;
                    bullet.transform.eulerAngles = normals[i];
                    bullet.gameObject.SetActive(true);
                }
            }
        }
    }
}
