using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	public GameObject Player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Platform" || other.gameObject.tag == "Pillar")
		{
            Destroy(other.gameObject);
            if (other.transform.parent.transform.parent)
            {
                Destroy(other.transform.parent.transform.parent.gameObject);
            }
            if (other.transform.parent) {
				Destroy (other.transform.parent.gameObject);
			}

		}
	}
}
