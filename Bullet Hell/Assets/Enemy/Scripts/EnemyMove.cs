using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

    public Vector3 Rotation = new Vector3(0, 200, 300);
    public float Speed = 2.0f;
    public float TimeBetweenPatterns = 2.0f;
    public EnemyFire[] SpawnPoints;
    public GameObject Player;

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

        // TODO: Figure out how to make the enemy look at you
        //float step = Speed * Time.deltaTime;
        //Vector3 lookDir = Player.transform.position - transform.position;
        //transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, lookDir, step, 0.0F));
    }

    private void GeneratePatternSettings()
    {
        foreach(EnemyFire e in SpawnPoints)
        {
            e.gameObject.SetActive(false);
        }

        armFireRate = Random.Range(0.01f, 0.1f);
        numArms = Random.Range(1, maxArms);
        for(int i = 0; i < numArms; i++)
        {
            int index = Random.Range(0, numArms);
            if(!SpawnPoints[index].gameObject.activeSelf)
            {
                SpawnPoints[index].fireRate = armFireRate;
                SpawnPoints[index].gameObject.SetActive(true);
            }
            else
            {
                i--;
            }
        }
        Rotation.y = Random.Range(45, 360);
        Rotation.z = Random.Range(45, 360);
        Speed = Random.Range(1, 3);
    }
}
