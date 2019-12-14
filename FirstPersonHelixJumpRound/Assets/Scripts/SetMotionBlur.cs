using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetMotionBlur : MonoBehaviour {
    Kino.Motion motionBlurScript;
    public float[] shutterAngleValues;//0 = low, 1 = medium, 2 = high
    public float[] MFBStrengthValues;
    public string[] TextList;
    public float MlGModeAngleValue;
    public float MLGModeStrengthValue;
    public bool locked = false;
    public TextMeshProUGUI motionBlurText;

    int rotateValue = 2;
	// Use this for initialization
	void Start () {
        motionBlurScript = Camera.main.GetComponent<Kino.Motion>();
        motionBlurText = this.gameObject.GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Lock()
    {
        motionBlurScript.enabled = true;
        locked = true;
        motionBlurText.text = "Motion Blur: MLG";
        motionBlurScript.frameBlending = MLGModeStrengthValue;
        motionBlurScript.shutterAngle = MlGModeAngleValue;
    }

    public void Unlock()
    {
        
        locked = false;
        motionBlurText.color = Color.white;
        int tempValue = (int)Mathf.Repeat(rotateValue, shutterAngleValues.Length);
        if (tempValue == 0)
        {
            motionBlurScript.enabled = false;
        }
        motionBlurText.text = TextList[tempValue];
        motionBlurScript.shutterAngle = shutterAngleValues[tempValue];
        motionBlurScript.frameBlending = MFBStrengthValues[tempValue];

    }
    public void clicked()
    {
        if(!locked)
        {
            motionBlurScript.enabled = true;
            rotateValue++;
            int tempValue = (int)Mathf.Repeat(rotateValue, shutterAngleValues.Length);
            motionBlurText.text = TextList[tempValue];
            motionBlurScript.shutterAngle = shutterAngleValues[tempValue];
            motionBlurScript.frameBlending = MFBStrengthValues[tempValue];
            if(tempValue == 0)
            {
                motionBlurScript.sampleCount = 0;
                motionBlurScript.enabled = false;
            }
            else
            {
                motionBlurScript.sampleCount = 8;
            }
        }
    }

}
