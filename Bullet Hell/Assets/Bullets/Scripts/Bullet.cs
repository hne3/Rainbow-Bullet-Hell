using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f; // Speed of bullet

    private float explosionRadius = 5.0f; // Area of bullets affected

    private int recursionMax = 200; // Max bullets you can eliminate with one punch
    
	private void OnEnable ()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Punch")
        {
            Explode();
        }
        else if(c.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
    }

    private void Explode()
    {
        // Explode all other bullets within radius
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider c in hits)
        {
            Bullet b = c.gameObject.GetComponentInParent<Bullet>();
            if (b)
            {
                b.gameObject.SetActive(false);
            }
        }
        // Explosion effect
        gameObject.SetActive(false);
    }
}
