using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyOut : MonoBehaviour {

	public Rigidbody rigid;
	public float FlyOutStrengthConstant = 1f;
    public float FlyUpStrengthConstant = 0.3f;
    public float FlyUpStrength = 1f;
	public float deleteAfter = 3.0f;

	public Renderer rend;

	public Transform rotation;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /*public void GetPlayerVelocity(float playerVelocity)
    {
        FlyOutStrength = -playerVelocity;

    }*/
	public void FlyAway(float FlyOutStrength){
		rotation = this.gameObject.transform;
        this.gameObject.layer = 8;
        if(GetComponent<Rigidbody>())//if this has a rigidbody, set rigid to it.
        {
            rigid = this.gameObject.GetComponent<Rigidbody>();
        }else
        {
            rigid = this.gameObject.AddComponent<Rigidbody>();
        }
        float randomAddition = Random.Range(-0.75f, 0.75f);
        rigid.AddRelativeForce (rotation.up * (-FlyOutStrength* FlyUpStrengthConstant + randomAddition), ForceMode.Impulse);
        rigid.AddRelativeForce (rotation.forward * (-FlyOutStrength * FlyOutStrengthConstant + randomAddition), ForceMode.Impulse);
        //rigid.AddForce(rotation.right * Random.Range(-0.20f, 0.20f), ForceMode.Impulse);
        //rigid.AddForce(rotation.right * -Random.Range(-0.10f, 0.40f), ForceMode.Impulse);
        //rigid.AddForce(rotation.right * -1.5f, ForceMode.Impulse);
        if (this.gameObject.transform.parent == true) {
			Destroy (this.gameObject.transform.parent.gameObject, deleteAfter);
		}
		Destroy (this.gameObject, deleteAfter);
	}
}
