using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question{
    public string question;
    public string answer;
    public string[] choices;
    public int speed;

    public Question(string q, string answer, string[] choices, int speed)
    {
        this.question = q;
        this.answer = answer;
        this.choices = choices;
        this.speed = speed;
    }
}
