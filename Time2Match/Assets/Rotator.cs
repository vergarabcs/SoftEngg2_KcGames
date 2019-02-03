using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    Vector3 rotEuler;
    public float rotSpeed;
    private void Start()
    {
        rotEuler = new Vector3(0, 0, rotSpeed);
    }
    private void Update()
    {
        this.gameObject.transform.Rotate(rotEuler);
    }
}
