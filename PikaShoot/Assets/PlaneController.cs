using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {
    public GameObject sphere;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private Collider ballCollider;
    private bool isFirstTime = true;
    private void OnTriggerEnter(Collider other)
    {
        if (isFirstTime) {
            isFirstTime = false;
            ballCollider = other;
            Invoke("ReplaceBall", 1.0f);
        }
    }

    void ReplaceBall()
    {
        this.isFirstTime = true;
        Destroy(ballCollider.gameObject);
        Instantiate(sphere);
    }
}
