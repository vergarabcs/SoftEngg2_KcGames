using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log{
    
    public static void print(string message)
    {
        GameObject go = GameObject.FindGameObjectWithTag("DebugText");
        Text tx = go.GetComponent<Text>();
        tx.text = tx.text+"\n"+message;
    }
}
