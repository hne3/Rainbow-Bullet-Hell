using UnityEngine;

public class FistOfFury : MonoBehaviour {

    public float Speed = 1.0f;

    public ForceField ForceFieldPrefab;

    public GameObject Enemy;

    private ForceField currentForceField;

	private void Update ()
    {
        Vector3 pos = transform.position;

        if(Input.GetKey(KeyCode.W))
        {
            pos.y += Time.deltaTime * Speed;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            pos.y -= Time.deltaTime * Speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            pos.x += Time.deltaTime * Speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            pos.x -= Time.deltaTime * Speed;
        }

        if (Input.GetKey(KeyCode.E))
        {
            pos.z += Time.deltaTime * Speed;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            pos.z -= Time.deltaTime * Speed;
        }

        transform.position = pos;

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
