using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class ChangeColor : MonoBehaviour {
    public Material good;
    public Material bad;

    public Image goodChild;
    public Image badChild;
	// Use this for initialization
    void Start () {
        goodChild = GetComponentsInChildren<Image>()[1];
        badChild = GetComponentsInChildren<Image>()[0];
        if (this.tag == "DefaultColor")
        {
            GetMat();//sets the starting material color to default. 
        }
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void GetMat()
    {
        good.color = goodChild.color;//when called by an object, will assign that object's goodchild's color as the good platform color.
        bad.color = badChild.color;
    }
}
