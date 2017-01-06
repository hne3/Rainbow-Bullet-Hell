using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

    public Vector3 Rotation = new Vector3(0, 200, 300);
    public float Speed = 2.0f;

	private void Update ()
    {
        transform.Rotate(Rotation * Time.deltaTime * Speed);
	}
}
