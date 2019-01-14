using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//790 to 900. -170 to 170
public class BallController : MonoBehaviour
{
    public GameObject prefab;
    public float forward;
    public float horizontal;
    private static float minXforce = -170.0f;
    private static float maxXforce = 170.0f;
    private static float minZforce = 790.0f;
    private static float maxZforce = 900.0f;
    private static float minMagnitude = minZforce;
    private static float maxMagnitude = Mathf.Sqrt(minZforce * minZforce + maxXforce * maxXforce);
    private static float maxTime = 500.0f;
    private static float minTime = 50.0f;

    // Use this for initialization
    void Start()
    {
        Physics.gravity = Vector3.down * 9.8f * 4.0f;
        //OnUp(Vector3.up);
        //Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        //rb.useGravity = true;
        //Vector3 force = new Vector3(horizontal, 800.0f, forward);
        //rb.AddForce(force);
    }

    // Update is called once per frame
    private bool isCalled = false;
    void Update()
    {
        if(Input.touchCount==0 || isCalled)
        {
            return;
        }
        foreach(Touch x in Input.touches)
        {
            if(TouchPhase.Began == x.phase)
            {
                OnDown(x.position);
            }else if(TouchPhase.Ended == x.phase)
            {
                OnUp(x.position);
            }
            return;
        }
    }
    
    private static Vector3 mouseBegin;
    private static long time = 0;
    private void OnDown(Vector3 position)
    {
        time = DateTime.Now.Millisecond;
        mouseBegin = position;
    }

    private void OnUp(Vector3 position)
    {
        isCalled = true;
        long elapsed = DateTime.Now.Millisecond - time;
        Vector3 offset = position - mouseBegin;
        print(offset);
        offset.Normalize();
        offset = offset * ((maxMagnitude - minMagnitude) / (maxTime - minTime) * (elapsed) + minMagnitude);
        print(offset);
        Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        Vector3 force = new Vector3(offset.x*0.6f, 800.0f, 820.0f);
        print(String.Format("Force: {0}", force));
        rb.AddForce(force*0.01f*2.0f, ForceMode.VelocityChange);
    }

    void OnMouseDown()
    {
        OnDown(Input.mousePosition);        
    }
    void OnMouseUp()
    {
        OnUp(Input.mousePosition);
    }
}
