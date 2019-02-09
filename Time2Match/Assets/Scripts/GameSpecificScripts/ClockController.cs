using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour {
    private Transform shortHand;
    private Transform longHand;
    public Transform radioButton;
    public int hour;
    public int minute;
    public LineRenderer line;
    public bool isDrawingState;
    public GameObject redGlow;
    public GameObject greenGlow;
    public GameObject UIHoverListener;

    private void Awake()
    {
        isDrawingState = false;
        shortHand = transform.GetChild(0);
        longHand = transform.GetChild(1);
        radioButton = transform.GetChild(2);
        line = transform.GetChild(3).GetComponent<LineRenderer>();
        line.SetWidth(0.10f, 0.10f);
        line.SetPosition(0, radioButton.position);
        line.enabled = false;
        SetHands();
    }

    private void Update()
    {
        if (isDrawingState)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.SetPosition(1, pos);
        }
    }

    private void SetHands()
    {
        float sDegrees = -(((float)hour/12.0f) * 360.0f);
        float lDegrees = -(((float)minute/60.0f) * 360.0f);
        shortHand.localRotation = Quaternion.Euler(0.0f, 0.0f, sDegrees);
        longHand.localRotation = Quaternion.Euler(0.0f, 0.0f, lDegrees);
    }


    public void SetTime(int hour, int minute)
    {
        this.hour = hour;
        this.minute = minute;
        SetHands();
    }

    public bool isBlocked()
    {
        UIHoverListener uiListener = UIHoverListener.GetComponent<UIHoverListener>();
        return uiListener.isUIOverride;
    }

    void OnMouseDown()
    {
        
        if (isBlocked())
        {
            Debug.Log("Cancelled OnMouseDown! A UI element has override this object!");
        }
        else
        {
            //Debug.Log("Object OnMouseDown");
            line.SetPosition(0, radioButton.position);
        }
    }

    void OnMouseUp()
    {
        print("clock up");
    }

    void OnMouseDrag()
    {
       
    }

    internal void showMark(bool isCorrect)
    {
        if (isCorrect)
        {
            redGlow.active = false;
            greenGlow.active = true;
        }
        else
        {
            greenGlow.active = false;
            redGlow.active = true;
        }
    }

    internal void ClearMark()
    {
        greenGlow.active = false;
        redGlow.active = false;
    }
}
