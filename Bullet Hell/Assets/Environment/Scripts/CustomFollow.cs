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

        transform.rotation = target.rotation;

        delta = transform.position - target.position;
    }
}