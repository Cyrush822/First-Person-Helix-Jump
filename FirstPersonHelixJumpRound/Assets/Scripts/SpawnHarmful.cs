using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHarmful : MonoBehaviour {

	public GameObject Platform;
	public GameObject spawnedPlatform;
	// Use this for initialization
	void Start () {
		spawnedPlatform = Instantiate(Platform, new Vector3(-0.3f,-0.3f,-0.3f), Quaternion.Euler(new Vector3(0,0,0)));
		spawnedPlatform.GetComponent<HarmfulPlatRotation> ().Direction = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
