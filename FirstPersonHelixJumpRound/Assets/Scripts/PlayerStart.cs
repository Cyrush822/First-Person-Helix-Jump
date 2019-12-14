using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStart : MonoBehaviour {

	// Use this for initialization
	public GameObject Platform1;
	public bool flashing = true;
    public GameObject startText;//flashing text
    public GameObject startingText;
    Spawner spawner;
	// Update is called once per frame


	void Start()
	{
        spawner = FindObjectOfType<Spawner>();
        StartCoroutine(textFlash());
        Camera.main.GetComponent<Camera>().depthTextureMode = DepthTextureMode.MotionVectors;
	}
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			Destroy (Platform1);
			flashing = false;
            startText.SetActive(false);
            startingText.SetActive(false);
            spawner.initialSpawn();  
		}
			
	}

	public IEnumerator textFlash ()
	{
		while(flashing == true)
		{
			yield return new WaitForSeconds (0.5f);
            if(flashing)
            {
                startText.SetActive(false);
            }
			yield return new WaitForSeconds (0.5f);

            if (flashing)
            {
                startText.SetActive(true);
            }
		}
	}
}
