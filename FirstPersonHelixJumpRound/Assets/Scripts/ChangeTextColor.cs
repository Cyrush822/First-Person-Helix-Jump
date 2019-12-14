using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChangeTextColor : MonoBehaviour {
    public Color changedColor;
    public Text[] textToChange;
    public TextMeshProUGUI[] textMeshProToChange;
    public TextMeshProUGUI motionBlurText;
    public Color normalColor;
    public float colorChangeSpeed;
    public Material good;
    public Material bad;
    public Image goodMLGImage;
    public Image badMLGImage;
    public bool MLGMODE;
    Color goodChangingColor;
    Color badChangingColor;

    Spawner spawner;
    public GameObject player;
    // Use this for initialization
    void Start () {
        DefaultTextColor();
        
    }
	
	// Update is called once per frame
	void Update () {
        goodChangingColor = Color.HSVToRGB(Mathf.PingPong(Time.time * colorChangeSpeed, 1), 1, 1);
        badChangingColor = Color.HSVToRGB(Mathf.PingPong((Time.time * colorChangeSpeed + 0.5f), 1), 0.75f, 0.75f);
        goodMLGImage.color = goodChangingColor;
        badMLGImage.color = badChangingColor;
        if (MLGMODE)
        {
            good.color = goodChangingColor;
            bad.color = badChangingColor;
            motionBlurText.color = badChangingColor;
            foreach (Text t in textToChange)
            {
                if(t.gameObject.tag != "Highscore")
                {
                    t.color = badChangingColor;
                }
            }
            foreach (TextMeshProUGUI tm in textMeshProToChange)
            {
                tm.color = badChangingColor;
            }
            
        }
    }

    public void updateTextColor()
    {
        spawner = player.GetComponent<Spawner>();
        MLGMODE = false;
        foreach (Text t in textToChange)
        {
            t.color = changedColor;

        }
        foreach (TextMeshProUGUI tm in textMeshProToChange)
        {
            tm.color = changedColor;
        }
        motionBlurText.gameObject.GetComponent<SetMotion2>().Unlock();
        FindObjectOfType<PlayerController>().doublePointsOff();
        spawner.UnforceDifficultyChange();

    }
    public void DefaultTextColor()
    {
        spawner = player.GetComponent<Spawner>();
        MLGMODE = false;
        foreach (Text t in textToChange)
        {
            t.color = normalColor;
        }
        foreach (TextMeshProUGUI tm in textMeshProToChange)
        {
            tm.color = normalColor;
        }
        motionBlurText.gameObject.GetComponent<SetMotion2>().Unlock();
        FindObjectOfType<PlayerController>().doublePointsOff();
        spawner.UnforceDifficultyChange();
    }
    public void MLGColor()
    {
        spawner = player.GetComponent<Spawner>();
        MLGMODE = true;
        motionBlurText.gameObject.GetComponent<SetMotion2>().Lock();
        //Camera.main.GetComponent<Kino.Motion>().sampleCount
        FindObjectOfType<PlayerController>().doublePointsOn();
        spawner.ForceDifficultyChange();
    }

}
