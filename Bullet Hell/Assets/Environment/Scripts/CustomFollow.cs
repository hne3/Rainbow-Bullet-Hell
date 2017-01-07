using UnityEngine;

public class CustomFollow : MonoBehaviour
{
    // The target we are following
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float distance = 1.0f;

    private Vector3 delta;

    void Start()
    {
        delta = transform.position - target.position;
        delta = delta.normalized * distance;
    }

    void LateUpdate()
    {

        transform.position = target.position + delta;

        Vector3 dist = new Vector3(Input.GetAxis("CameraVertical"), Input.GetAxis("CameraHorizontal"), 0.0f);

        transform.RotateAround(target.position, transform.right, dist.x);
        transform.RotateAround(target.position, transform.up, dist.y);

        transform.LookAt(target);

        delta = transform.position - target.position;
    }
}