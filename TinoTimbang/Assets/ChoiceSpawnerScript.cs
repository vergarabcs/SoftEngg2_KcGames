using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceSpawnerScript : MonoBehaviour {
    private static float ROW_DISTANCE = 2.0f;
    private static float COL_DISTANCE = 2.0F;
    private static int colCount = 2;
    public List<GameObject> choices;

    private void Start()
    {
        choices = new List<GameObject>();
        DataController dc = GameObject.FindObjectOfType<DataController>();
        Question q = dc.getCurrentQuestion();
        float x = transform.position.x;
        float y = transform.position.y;
        int ctr = 0;
        foreach (GameObject gObj in q.prefabs)
        {
            float x_mult = (float)(ctr % colCount);
            float y_mult = (float)(ctr / colCount);
            float true_x = x + COL_DISTANCE * x_mult;
            float true_y = y - ROW_DISTANCE * y_mult;
            choices.Add(Instantiate(gObj, new Vector3(true_x, true_y, 0.0f), Quaternion.identity));
            ctr++;
        }
    }
}
