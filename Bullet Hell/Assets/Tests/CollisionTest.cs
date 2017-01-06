using UnityEngine;
using System.Collections;

public class CollisionTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddForce(new Vector3(-100, 0, 0));
	}

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
