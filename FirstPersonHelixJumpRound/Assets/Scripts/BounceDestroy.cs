using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceDestroy : MonoBehaviour {
    float flyOutMagnitude;
    bool destroyBottomActivate;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.tag == "Platform")
		{
            if (destroyBottomActivate == true) {
                if(other.gameObject.GetComponent<FlyOut>() == true)
                {
                    other.gameObject.GetComponent<FlyOut>().FlyAway(flyOutMagnitude);
                   // other.gameObject.GetComponent<FlyOut>().GetPlayerVelocity(this.GetComponentInParent<Rigidbody>().velocity.y);
                    StartCoroutine(disable());
                }

			}
		}
	}
    public void ActivateDestroy(float collisionY)
    {
        destroyBottomActivate = true;
        flyOutMagnitude = collisionY;
    }

	IEnumerator disable()
	{
		yield return new WaitForSeconds (0.01f);
        destroyBottomActivate = false;
	}
}
