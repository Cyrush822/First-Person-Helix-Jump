using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

    public float rotateSpeed = 2f;
    public KeyCode left;
    public KeyCode right;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey (left)) {
            transform.Rotate (Vector3.up * rotateSpeed * Time.deltaTime);
		}

		if (Input.GetKey (right)) {
            transform.Rotate (Vector3.up * -rotateSpeed * Time.deltaTime);
		}
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * -rotateSpeed * Time.deltaTime);
        }

    }
}
