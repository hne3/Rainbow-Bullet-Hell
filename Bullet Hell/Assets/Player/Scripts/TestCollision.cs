using UnityEngine;
using System.Collections;

public class TestCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddForce(100, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
