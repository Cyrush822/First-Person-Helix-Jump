using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PostProcessing;

public class SetMotion2 : MonoBehaviour
{
    PostProcessingProfile motionBlurScript;
    PostProcessingBehaviour PPbehavior;
    public float[] shutterAngleValues;//0 = low, 1 = medium, 2 = high
    public float[] MFBStrengthValues;
    public string[] TextList;
    public float MlGModeAngleValue;
    public float MLGModeStrengthValue;
    public bool locked = false;
    public TextMeshProUGUI motionBlurText;

    int rotateValue = 2;
    // Use this for initialization
    void Start()
    {
        PPbehavior = Camera.main.GetComponent<PostProcessingBehaviour>();
        motionBlurScript = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
        motionBlurText = this.gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Lock()
    {
        PPbehavior.enabled = true;
        locked = true;
        motionBlurText.text = "Motion Blur: MLG";
        MotionBlurModel.Settings a = motionBlurScript.motionBlur.settings;
        a.frameBlending = MLGModeStrengthValue;
        a.shutterAngle = MlGModeAngleValue;
        motionBlurScript.motionBlur.settings = a;
    }

    public void Unlock()
    {

        locked = false;
        motionBlurText.color = Color.white;
        int tempValue = (int)Mathf.Repeat(rotateValue, shutterAngleValues.Length);
        if (tempValue == 0)
        {
            PPbehavior.enabled = false;
        }
        motionBlurText.text = TextList[tempValue];
        MotionBlurModel.Settings a = motionBlurScript.motionBlur.settings;
        a.shutterAngle = shutterAngleValues[tempValue];
        a.frameBlending = MFBStrengthValues[tempValue];
        motionBlurScript.motionBlur.settings = a;

    }
    public void clicked()
    {
        if (!locked)
        {
            PPbehavior.enabled = true;
            rotateValue++;
            int tempValue = (int)Mathf.Repeat(rotateValue, shutterAngleValues.Length);
            motionBlurText.text = TextList[tempValue];
            MotionBlurModel.Settings a = motionBlurScript.motionBlur.settings;
            a.shutterAngle = shutterAngleValues[tempValue];
            a.frameBlending = MFBStrengthValues[tempValue];
            if (tempValue == 0)
            {

                a.sampleCount = 0;
                PPbehavior.enabled = false;
            }
            else
            {
                a.sampleCount = 8;
            }
            motionBlurScript.motionBlur.settings = a;
        }
    }

}
