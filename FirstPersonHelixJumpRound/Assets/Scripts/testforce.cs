using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testforce : MonoBehaviour {
	public Rigidbody rigid;
	public Transform direction;
	// Use this for initialization
	void Start () {
		rigid = this.gameObject.AddComponent<Rigidbody>();
		direction = this.gameObject.transform;
		rigid.AddForce (-direction.up * 10f, ForceMode.Impulse);
		Destroy (this.gameObject, 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
