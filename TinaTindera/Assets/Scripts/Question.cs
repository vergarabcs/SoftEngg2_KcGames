using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Question
{
    public string question;
    public int[] fruitCounts;
    public string[] fruitAnswers;
    public Question(string question, int[] fruitCounts, string[] fruitAnswers)
    {
        this.question = question;
        this.fruitCounts = fruitCounts;
        this.fruitAnswers = fruitAnswers;
    }

    public override string ToString()
    {
        return string.Format("{0} - Answer: {1} - {2}", question, fruitCounts, fruitAnswers);
    }
}