using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    public float secondsMainGame;
	// Use this for initialization
	void Start () {
        Invoke("MainScene", secondsMainGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void MainScene()
    {
        SceneManager.LoadScene("MainGame");
    }
}
