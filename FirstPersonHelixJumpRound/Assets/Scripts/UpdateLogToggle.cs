using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLogToggle : MonoBehaviour {
    PlayerController playerControlScript;
    Rotation rotateScript;
    Spawner spawnScript;
    PlayerStart startScript;
    public GameObject UpdateLog;
	// Use this for initialization
	void Start () {
        rotateScript = FindObjectOfType<Rotation>();
        spawnScript = FindObjectOfType<Spawner>();
        playerControlScript = FindObjectOfType<PlayerController>();
        startScript = FindObjectOfType<PlayerStart>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleOn(){
        //playerControlScript.enabled = false;
        rotateScript.enabled = false;
        spawnScript.enabled = false;
        startScript.enabled = false;
        UpdateLog.SetActive(true);
    }

    public void ToggleOff()
    {
        //playerControlScript.enabled = true;
        rotateScript.enabled = true;
        spawnScript.enabled = true;
        startScript.enabled = true;
        UpdateLog.SetActive(false);
    }
}
