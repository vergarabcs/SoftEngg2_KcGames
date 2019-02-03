using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleQuestion : MonoBehaviour {
    public GameObject[] prefabs;

    private static float ROW_DISTANCE = 2.0f;
    private static float COL_DISTANCE = 2.0F;
    private static int colCount = 2;

    private void Start()
    {
        DataController dc = GameObject.FindObjectOfType<DataController>();
        GameObject[] gameObjects = prefabs;
        float x = transform.position.x;
        float y = transform.position.y;
        int ctr = 0;
        foreach (GameObject gObj in gameObjects)
        {
            float x_mult = (float)(ctr % colCount);
            float y_mult = (float)(ctr / colCount);
            float true_x = x + COL_DISTANCE * x_mult;
            float true_y = y - ROW_DISTANCE * y_mult;
            Instantiate(gObj, new Vector3(true_x, true_y, 0.0f), Quaternion.identity);
            ctr++;
        }
    }

    public Question sampleQ()
    {
        Question q = new Question();
        q.prefabs = this.prefabs;
        q.difficulty = Utility.DIFFICULT;
        return q;
    }
}
