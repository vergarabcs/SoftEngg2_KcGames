using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour {
    private Transform shortHand;
    private Transform longHand;
    public int hour;
    public int minute;
    

    private void Awake()
    {
        shortHand = transform.GetChild(0);
        longHand = transform.GetChild(1);
        SetHands();
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

    internal void DisableRotation(bool isShortHand)
    {
        InputRotator ir;
        BoxCollider2D bc;
        if (isShortHand)
        {
             ir = shortHand.GetComponent<InputRotator>();
            bc = shortHand.GetComponent<BoxCollider2D>();
        }
        else
        {
             ir = longHand.GetComponent<InputRotator>();
            bc = longHand.GetComponent<BoxCollider2D>();
        }
        bc.enabled = false;
    }
}
