using UnityEngine;
using System.Collections;

public class TestRNG : MonoBehaviour {

    public GameObject test;
    public Material testMaterial;

    private void Start()
    {
        testMaterial = test.GetComponent<Renderer>().material;
    }

    void Update () {
        Debug.Log(testMaterial.GetFloat("_NewSeed"));
	}
}
