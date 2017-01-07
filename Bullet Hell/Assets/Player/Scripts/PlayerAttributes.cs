using UnityEngine;
using System.Collections;

public class PlayerAttributes : MonoBehaviour {

    public float MaxHealth = 100.0f;
    public float DamagePerHit = 5.0f;

    private float currentHealth;

	private void Start ()
    {
        currentHealth = MaxHealth;
	}
	
	private void Update ()
    {
	
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            currentHealth -= DamagePerHit;
        }

    }
}
