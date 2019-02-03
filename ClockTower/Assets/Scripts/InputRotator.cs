using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRotator : MonoBehaviour {
 
    void OnMouseDrag()
    {
        Vector3 currentDragDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        currentDragDir.z = 0;
        this.transform.up = currentDragDir; 
    }

    //ROUND OFF TO nearest valid hand position code is here
    private void OnMouseUp()
    {
        float angleDegrees = this.transform.rotation.eulerAngles.z;
        float anglePerTick = 360.0f / 4.0f;
        if (this.gameObject.name == "ShortHand")
            anglePerTick = 360.0f / 12.0f;
         
        float temp = angleDegrees / (anglePerTick);
        temp = Mathf.Round(temp);
        angleDegrees = temp * anglePerTick;
        angleDegrees %= 360;

        SetClockTime(angleDegrees);
    }

    private void SetClockTime(float angleDegrees)
    {
        ClockController cc = this.GetComponentInParent<ClockController>();
        int hour = cc.hour;
        int min = cc.minute;
        if (this.gameObject.name == "ShortHand")
        {
            hour = (int)((12.0f - (angleDegrees * 12.0f / 360.0f)) % 12);
        }
        else
        {
            min = (int)((60.0f - (angleDegrees*60.0f/360.0f)) % 60);
        }
        cc.SetTime(hour, min);
    }
}
