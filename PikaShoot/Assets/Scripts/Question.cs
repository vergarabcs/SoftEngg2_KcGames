using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question{
    public DateTime dateTime;
    public string question;
    public string answer;
    public string[] choices;

    //diameter of trash cans?
    public float diameter;

    public Question(string q, string answer, string[] choices, float diameter, DateTime dateTime)
    {
        this.dateTime = dateTime;
        this.question = q;
        this.answer = answer;
        this.choices = choices;
        this.diameter = diameter;
    }
}
