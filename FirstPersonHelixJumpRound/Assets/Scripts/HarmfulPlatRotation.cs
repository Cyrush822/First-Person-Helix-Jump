using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmfulPlatRotation : MonoBehaviour {

	public float platRotation;
	public float platRotationSpeedPerSec = 40f;
	public float platRotationSpeedPerSecDefault;

    public float rotationrange = 45;
	public float changeRotationTimer = 1f;
	public float timerCompare;

	public int Direction = 0; //0 = no movement, 1 = right, 2 = left
	// Use this for initialization
	void Start () {
		platRotationSpeedPerSecDefault = platRotationSpeedPerSec;
	}
	
	// Update is called once per frame
	void Update () {
		if (Direction == 1) {
			transform.Rotate (new Vector3 (0, platRotationSpeedPerSec * Time.deltaTime,0));
			platRotation += platRotationSpeedPerSec * Time.deltaTime;
			if (platRotation > rotationrange && timerCompare < Time.fixedTime) {
				platRotationSpeedPerSec = -platRotationSpeedPerSecDefault;
				timerCompare = Time.fixedTime + changeRotationTimer;
			}
			if (platRotation < 0f && timerCompare < Time.fixedTime) {
				platRotationSpeedPerSec = platRotationSpeedPerSecDefault;
				timerCompare = Time.fixedTime + changeRotationTimer;
			}
		}
		if (Direction == 2) {
			transform.Rotate (new Vector3 (0, -platRotationSpeedPerSec * Time.deltaTime, 0));
			platRotation += -platRotationSpeedPerSec * Time.deltaTime;
			if (platRotation < -rotationrange && timerCompare < Time.fixedTime) {
				platRotationSpeedPerSec = -platRotationSpeedPerSecDefault;
				timerCompare = Time.fixedTime + changeRotationTimer;
			}
			if (platRotation > 0f && timerCompare < Time.fixedTime) {
				platRotationSpeedPerSec = platRotationSpeedPerSecDefault;
				timerCompare = Time.fixedTime + changeRotationTimer;
			}
		}
        if (Direction == 3)
        {
            transform.Rotate(new Vector3(0, -platRotationSpeedPerSec * Time.deltaTime, 0));
            platRotation += -platRotationSpeedPerSec * Time.deltaTime;
            if (platRotation < -rotationrange && timerCompare < Time.fixedTime)
            {
                platRotationSpeedPerSec = -platRotationSpeedPerSecDefault;
                timerCompare = Time.fixedTime + changeRotationTimer;
            }
            if (platRotation > rotationrange && timerCompare < Time.fixedTime)
            {
                platRotationSpeedPerSec = platRotationSpeedPerSecDefault;
                timerCompare = Time.fixedTime + changeRotationTimer;
            }
        }

        //clamp is so that it won't go beyond 0 or 45.
    }
}
