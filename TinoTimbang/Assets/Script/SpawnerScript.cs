using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {
    private static Vector3 spawnLocation;
    LevelController lc;
    Rigidbody2D rb2d;
    public bool isAdded = false;

    private void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        lc = GameObject.FindObjectOfType<LevelController>();
        GameObject cl = GameObject.Find("ContainerLeft");
        spawnLocation = cl.transform.position;
        spawnLocation.y = spawnLocation.y + 4.0f;
    }

    private void OnMouseDown()
    {
        if (isAdded)
        {
            Destroy(this.gameObject);
            lc.removeFromLeft(rb2d.mass);
        }
        else
        {
            GameObject instance = Instantiate(this.gameObject);
            SpawnerScript ss = instance.GetComponent<SpawnerScript>();
            ss.isAdded = true;
            instance.transform.position = spawnLocation;
            Rigidbody2D rb2d = instance.GetComponent<Rigidbody2D>();
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            lc.addToLeft(rb2d.mass);
        }
    }
}
