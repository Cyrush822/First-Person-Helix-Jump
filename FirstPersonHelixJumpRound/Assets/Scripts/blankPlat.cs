using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blankPlat : MonoBehaviour {

	public PlayerController activate;
	public GameObject spherePlayer;
	public Spawner spawner;
	// Use this for initialization
	void Start () {
		activate = spherePlayer.GetComponent<PlayerController>();
		spawner = spherePlayer.GetComponent<Spawner> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.tag == "Sensor") {
			activate.Points ();
			spawner.SpawnPlatform ();
			Destroy (this.transform.parent.gameObject);

		}
	}
		
}
