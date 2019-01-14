using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;

public class RoundController : MonoBehaviour {
    public static float BIN_DISTANCE = -2.90f;
    GameController callBack;
    public GameObject prefab;
    public GameObject ball;
    static GameObject[] gObjects;
    public Question question;

	// Use this for initialization
	void Start () {
        //Question question = new Question("question", "answer", new string[] { "choice1", "choice2", "choice3" }, 7.0f, new DateTime(2015, 2, 6));
        //this.question = question;
        DataController dc = FindObjectOfType<DataController>();
        this.question = dc.questionPool[dc.questionIndex];
        ManualStart();
	}

    public void Initiate(Question question, GameController callBack=null)
    {
        this.callBack = callBack;
        this.question = question;
        ManualStart();
    }

    private void ManualStart()
    {
        gObjects = new GameObject[question.choices.Length];
        setQuestion(question.question);
        setCalendarDate();
        float pos = -BIN_DISTANCE;
        for(int i=0; i<3; i++)
        {
            GameObject gO = Instantiate(prefab, new Vector3(pos, 0.0f), Quaternion.identity);
            TrashController bc = gO.GetComponent<TrashController>();
            bc.SetText(question.choices[i]);
            pos += BIN_DISTANCE;
        }
        Instantiate(ball);
    }

    private void setCalendarDate()
    {
        GameObject tmP = GameObject.FindGameObjectWithTag("Calendar");
        Calendar controller = tmP.GetComponent<Calendar>();
        controller.targetDate = question.dateTime;
        controller.today = controller.targetDate;
        controller.UpdateCalandarDisplay();
    }

    private void setQuestion(string question)
    {
        GameObject tmP = GameObject.FindGameObjectWithTag("Question");
        tmP.GetComponent<Text>().text = question;
    }
    
    void Update()
    {
      
    }

   
}
