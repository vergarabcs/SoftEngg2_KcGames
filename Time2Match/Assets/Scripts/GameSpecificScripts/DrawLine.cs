using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour
{
    //reference to LineRenderer component
    private LineRenderer line;
    //car to store mouse position on the screen
    private Vector3 mousePos;
    //assign a material to the Line Renderer in the Inspector
    public Material material;
    //number of lines drawn
    private int currLines = 0;
    private ClockController clockController;
    public GameObject particlePrefab;

    public Vector3 startPos;
    public Vector3 endPos;
 
    void Update()
    {
        //Create new Line on left mouse click(down)
        if (Input.GetMouseButtonDown(0))
        {
            //DoesCollide();
            ////check if there is no line renderer created
            //if (line == null)
            //{
            //    //create the line
            //    createLine();
            //}
            ////get the mouse position
            //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ////set the z co ordinate to 0 as we are only interested in the xy axes
            //mousePos.z = 0;
            ////set the start point and end point of the line renderer
            //line.SetPosition(0, startPos);
            //line.SetPosition(1, mousePos);
            //print("drawlinedown");
            line = null;
            Collider2D collider2D = collidedWith();
            if (collider2D != null && collider2D.CompareTag("Clock"))
            {
                clockController = collider2D.gameObject.GetComponent<ClockController>();
                if (clockController.isBlocked()) return;
                clockController.line.enabled = true;
                clockController.isDrawingState = true;
                line = clockController.line;
            }
        }
        //if line renderer exists and left mouse button is click exited (up)
        else if (Input.GetMouseButtonUp(0) && line)
        {
            //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //mousePos.z = 0;
            ////set the end point of the line renderer to current mouse position
            //line.SetPosition(1, endPos);
            ////set line as null once the line is created
            //line.enabled = false;
            //currLines++;
            LevelController lc = GameObject.FindObjectOfType<LevelController>();
            Collider2D collider2D = collidedWith();
            if (collider2D != null && collider2D.CompareTag("Choice"))
            {
                ChoiceController cc = collider2D.gameObject.GetComponent<ChoiceController>();
                line.SetPosition(1, cc.radioButton.position);
                clockController.isDrawingState = false;
                lc.answerChecker.Match(cc, clockController);
            }
            else
            {
                //disable currently drawing line
                line.enabled = false;
                clockController.isDrawingState = false;
                lc.answerChecker.unMatch(clockController);
            }
        }
    }

    Collider2D collidedWith()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        return hit.collider;
    }

    //method to create line
    public LineRenderer createLine()
    {
        //create a new empty gameobject and line renderer component
        line = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
        //assign the material to the line
        line.material = material;
        //set the number of points to the line
        line.SetVertexCount(2);
        //set the width
        line.SetWidth(0.15f, 0.15f);
        //render line to the world origin and not to the object's position
        line.useWorldSpace = true;
        return line;
    }
}