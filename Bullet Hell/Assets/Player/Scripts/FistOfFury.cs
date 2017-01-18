using System.Collections;
using UnityEngine;

public class FistOfFury : MonoBehaviour {

    public float Speed = 1.0f;

    public float Distance = 5.0f;

    public float PunchRadius = 1.0f;

    public float TimeBetweenExplosions = 0.4f;

    public ForceField ForceFieldPrefab;

    public GameObject Enemy;

    private ForceField currentForceField;

    private Vector3 pos;

    private WaitForSeconds ExplosionWait;


    private void Start()
    {
        pos = new Vector3(0, 0, Distance);

        ExplosionWait = new WaitForSeconds(TimeBetweenExplosions);
    }
    
	private void Update ()
    {
        pos.x = Input.mousePosition.x;
        pos.y = Input.mousePosition.y;

        if (Input.GetKey(KeyCode.E))
        {
            pos.z += Time.deltaTime * Speed;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            pos.z -= Time.deltaTime * Speed;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("Punch");
        }

        transform.position = Camera.main.ScreenToWorldPoint(pos);

        float step = Speed * Time.deltaTime;
        Vector3 lookDir = Enemy.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, lookDir, step, 0.0F));

        if (Input.GetMouseButtonDown(0))
        {
            currentForceField = Instantiate(ForceFieldPrefab, transform.position, transform.rotation, transform) as ForceField;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(currentForceField.gameObject);
        }
    }

    private IEnumerator Punch()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, PunchRadius);
        foreach (Collider c in col)
        {
            Bullet b = c.GetComponent<Bullet>();
            if (b && b.isActiveAndEnabled)
            {
                b.StartCoroutine("Explode");
            }
        }
        yield break;
    }
}
