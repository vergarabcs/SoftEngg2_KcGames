using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCollisionControl : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Collider2D c = GetComponent<Collider2D>();
        GameObject[] arr = GameObject.FindGameObjectsWithTag("Fruit");
        foreach (GameObject x in arr)
        {
            Physics2D.IgnoreCollision(x.GetComponent<Collider2D>(), c);
        }
        arr = GameObject.FindGameObjectsWithTag("InnerCollider");
        foreach (GameObject x in arr)
        {
            Physics2D.IgnoreCollision(x.GetComponent<Collider2D>(), c);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    //try ignore collision on start
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit") || collision.gameObject.CompareTag("InnerCollider"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}