using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BubbleController : MonoBehaviour {
    public string text;
	// Use this for initialization
	void Start () {
        
	}

    

    internal void SetText(string v)
    {
        GameObject tmP = transform.GetChild(0).gameObject;
        tmP.GetComponent<TextMeshPro>().SetText(v);
        this.text = v;
    }
}
