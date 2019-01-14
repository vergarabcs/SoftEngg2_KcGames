using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceController : MonoBehaviour {
    public int hour;
    public int minute;
    private DrawLine drawLine;
    public Transform radioButton;

	// Use this for initialization
	void Start () {
        drawLine = GameObject.FindObjectOfType<DrawLine>();
        radioButton = transform.GetChild(0);
	}
    
    public void SetTime(int hour, int minute)
    {
        this.hour = hour;
        this.minute = minute;
        TextMeshPro tmp = GetComponent<TextMeshPro>();
        tmp.text = Utility.timeToString(hour, minute);
    }

    private void OnMouseUp()
    {
        print("choice up");
    }

    private void OnMouseDown()
    {
        print("choice down");
    }
}
