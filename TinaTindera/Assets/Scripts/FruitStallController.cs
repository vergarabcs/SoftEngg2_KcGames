using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitStallController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject[] array1 = GameObject.FindGameObjectsWithTag("Fruit");
        GameObject[] array2 = GameObject.FindGameObjectsWithTag("InnerCollider");
        for(int i=0; i<array1.Length-1; i++)
        {
            for(int j=i+1; j<array1.Length; j++)
            {
                Physics2D.IgnoreCollision(array1[i].GetComponent<Collider2D>(),
                    array1[j].GetComponent<Collider2D>());
            }
        }

        for(int i=0; i<array1.Length; i++)
        {
            for(int j=0; j<array2.Length; j++)
            {
                Physics2D.IgnoreCollision(array1[i].GetComponent<Collider2D>(),
                    array2[j].GetComponent<Collider2D>());
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
