using UnityEngine;

public class FistOfFury : MonoBehaviour {

    public float Speed = 1.0f;

    public float Distance = 5.0f;

    public ForceField ForceFieldPrefab;

    public GameObject Enemy;

    private ForceField currentForceField;

    private Vector3 pos;

    private void Start()
    {
        pos = new Vector3(0, 0, Distance);
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
}
