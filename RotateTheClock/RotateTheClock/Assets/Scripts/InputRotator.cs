using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRotator : MonoBehaviour {
 
    void OnMouseDrag()
    {
        
        print("mouseDrag");
        Vector3 currentDragDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        currentDragDir.z = 0;
        this.transform.up = currentDragDir; 
    }

    //ROUND OFF TO nearest valid hand position code is here
    private void OnMouseUp()
    {
        print("mouseUp");
        float angleDegrees = this.transform.rotation.eulerAngles.z;
        print(angleDegrees);
        float anglePerTick = 360.0f / 4.0f;
        if (this.gameObject.name == "ShortHand")
            anglePerTick = 360.0f / 12.0f;
         
        float temp = angleDegrees / (anglePerTick);
        temp = Mathf.Round(temp);
        angleDegrees = temp * anglePerTick;
        print(angleDegrees);
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angleDegrees);
    }
}
