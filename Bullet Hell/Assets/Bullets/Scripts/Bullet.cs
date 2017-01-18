using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem Explosion;

    public float Speed = 1f; // Speed of bullet

    private WaitForSeconds waitTime;

    private void Start()
    {
        waitTime = new WaitForSeconds(Explosion.startLifetime);
    }
    
	private void OnEnable ()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
    }

    private void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
        else
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), c.collider);
        }
    }

    public IEnumerator Explode()
    {
        Explosion.Play();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().rotation = Quaternion.identity;
        yield return waitTime;
        gameObject.SetActive(false);
        yield break;
    }
}
