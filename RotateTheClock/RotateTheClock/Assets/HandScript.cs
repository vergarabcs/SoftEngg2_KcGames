using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {
    public GameObject center;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        transform.LookAt(Input.mousePosition, Vector3.right);

    }

    private void OnMouseDrag()
    {
        transform.LookAt(Input.mousePosition, Vector3.right);
    }
}
