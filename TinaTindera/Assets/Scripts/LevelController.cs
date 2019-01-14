using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    Dictionary<string, int> dict;
    private DataController dc;
    Question q;
    public Text qText;

	// Use this for initialization
	void Start () {
        dict = new Dictionary<string, int>();
        dc = GameObject.FindObjectOfType<DataController>();
        q = dc.getCurrentQuestion();
        qText.text = q.question;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void SubmitAnswer()
    {
        if (!isCorrect())
        {
            dc.DidAnswer(false);
            return;
        }
        dc.DidAnswer(true);
    }

    private bool isCorrect()
    {
        if (dict.Keys.Count != q.fruitAnswers.Length)
            return false;
        for(int i=0; i<q.fruitAnswers.Length; i++)
        {
            string x = q.fruitAnswers[i];
            if (!dict.ContainsKey(x))
                return false;
            if (dict[x] != q.fruitCounts[i])
                return false;
        }
        return true;
    }

    internal void AddFruit(string fruitOnDropArea)
    {
       
        string fruitName = fruitOnDropArea.Split(' ')[0];
        if (dict.ContainsKey(fruitName))
        {
            dict[fruitName] = dict[fruitName] + 1;
        }
        else
        {
            dict.Add(fruitName, 1);
        }
    }
}
