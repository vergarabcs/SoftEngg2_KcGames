using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {
    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        Vector3 pos = Input.mousePosition;
        pos.x += 35;
        pos.y -= 50;
        this.transform.position = pos;
    }
}
