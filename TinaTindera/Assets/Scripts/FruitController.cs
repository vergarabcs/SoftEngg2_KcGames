using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FruitController : MonoBehaviour {
    Vector3 screenSpace;
    Vector3 offset;
    private Rigidbody2D rb2d;
    private bool isOnDropArea = false;
    private Vector3 startingPosition;
    private Updater noMotionUpdater;
    private Updater motionUpdater;
    private Updater currentUpdater;
    private string fruitOnDropArea;

    private interface Updater
    {
        void update();
    }

    private class NoMotionUpdater : Updater
    {
        public void update()
        {
            //empty. object is not moving
        }
    }

    private class MoveUpdater : Updater
    {
        private FruitController outerClass;
        public MoveUpdater(FruitController fc)
        {
            this.outerClass = fc;
        }
        public void update()
        {
            outerClass.transform.position = Vector3.MoveTowards(outerClass.transform.position, outerClass.startingPosition, 1.0f);
            if (outerClass.transform.position == outerClass.startingPosition)
            {
                //Motion ended, replace updater with no motion updater
                outerClass.currentUpdater = outerClass.noMotionUpdater;
                outerClass.rb2d.isKinematic = false;
            }
        }
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        noMotionUpdater = new NoMotionUpdater();
        motionUpdater = new MoveUpdater(this);
        currentUpdater = noMotionUpdater;
    }

    private void Update()
    {
        currentUpdater.update();
    }
    void OnMouseDown()
    {
        print("mouseDown");
        //translate the cubes position from the world to Screen Point
        this.startingPosition = this.transform.position;
        print("Starting position:" + startingPosition);
        rb2d.isKinematic = true;
        screenSpace = Camera.main.WorldToScreenPoint(this.transform.position);
        //calculate any difference between the cubes world position and the mouses Screen position converted to a world point  
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    }



    /*
    OnMouseDrag is called when the user has clicked on a GUIElement or Collider and is still holding down the mouse.
    OnMouseDrag is called every frame while the mouse is down.
    */
    void OnDrag()
    {

    }

    void OnMouseDrag()
    {

        //keep track of the mouse position
        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);

        //convert the screen mouse position to world point and adjust with offset
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + this.offset;

        //update the position of the object in the world
        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        print("mouseUp: " + startingPosition);
        print("isOnDropArea" + isOnDropArea);
        if (!isOnDropArea)
        {
            currentUpdater = motionUpdater;
        }
        else
        {
            rb2d.isKinematic = false;
            LevelController lc = GameObject.FindObjectOfType<LevelController>();
            lc.AddFruit(fruitOnDropArea);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.CompareTag("DropArea"))
        {
            print("dropArea");
            isOnDropArea = true;
            fruitOnDropArea = this.name;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DropArea"))
        {
            print("exitDropArea");
            isOnDropArea = false;
            fruitOnDropArea = "";
        }
    }
}
