using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class DataController : MonoBehaviour {
    public Question[] allRoundData;
    private static string[] months = { "Enero", "Pebrero", "Marso", "Abril", "Mayo", "Hunyo", "Hulyo", "Agosto", "Setyembre", "Oktubre", "Nobyembre", "Disyembre" };
    private static string[] days = { "Linggo", "Lunes", "Martes", "Miyerkules", "Huwebes", "Biyernes", "Sabado" };
    private HashSet<string> askedQuestions;

    private void Start()
    {
        askedQuestions = new HashSet<string>();
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Menuscreen");
    }

    public Question[] getQuestions()
    {
        int[] difficulties = generateRandomDifficulty();
        Question[] questions = new Question[10];
        for(int i=0; i<difficulties.Length; i++)
        {
            Question q = GenerateQuestion(difficulties[i]);
            while (askedQuestions.Contains(q.question))
            {
                q = GenerateQuestion(difficulties[i]);
            }
            questions[i] = q;
        }
        return questions;
    }

    private int[] generateRandomDifficulty()
    {
        List<int> difficulties = new List<int>();
        for(int i=0; i<5; i++)
        {
            difficulties.Add(0);
        }
        for (int i = 0; i < 3; i++)
        {
            difficulties.Add(1);
        }
        for (int i = 0; i < 2; i++)
        {
            difficulties.Add(2);
        }
        int[] diffArr = difficulties.ToArray();
        reshuffle(diffArr);
        return diffArr;
    }

    void reshuffle(int[] arr)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < arr.Length; t++)
        {
            int tmp = arr[t];
            int r = Random.Range(t, arr.Length);
            arr[t] = arr[r];
            arr[r] = tmp;
        }
    }

    private Question GenerateQuestion(int difficulty)
    {
        int choice_count = 0;
        int speed = 0;
        if (difficulty == 0)
        {
            choice_count = 2;
            speed = 30;
        }else if (difficulty == 1)
        {
            choice_count = 3;
            speed = 40;
        }
        else
        {
            choice_count = 4;
            speed = 50;
        }
        int num1 = Random.Range(0, 2); //isDay
        int num2 = Random.Range(0, 2);
        string dOrMQ = "buwan";
        int rand_i = Random.Range(0, months.Length);
        string dOrM = months[rand_i];
        string answer = months[(rand_i + 1) % months.Length];
        if (num1 == 0)
        {
            dOrMQ = "araw";
            rand_i = Random.Range(0, days.Length);
            dOrM = days[rand_i];
            answer = days[(rand_i + 1) % days.Length];
        }
        string q = String.Format("Ano ang {0} pagkatapos ng {1}", dOrMQ, dOrM);
        if (num2 == 0)
        {
            if (num1 == 0)
            {
                answer = (rand_i == 0) ? days[days.Length - 1] : days[rand_i - 1];
            }
            else
            {
                answer = (rand_i == 0) ? months[months.Length - 1] : months[rand_i - 1];
            }
            q = String.Format("Ano ang {0} bago ang {1}", dOrMQ, dOrM);
        }
        string[] choices = new string[choice_count];
        choices[0] = answer;

        if (num1 == 0)
        {
            RandomPick(choices, days);
        }
        else
        {
            RandomPick(choices, months);
        }

        Question qObj = new Question(q, answer, choices, speed);
        return qObj;
    }

    private void RandomPick(string[] choices, string[] daysOrMonths)
    {
        for (int i = 1; i < choices.Length; i++)
        {
            int rand_i = Random.Range(0, daysOrMonths.Length);
            while (contains(choices, daysOrMonths[rand_i]))
            {
                rand_i = Random.Range(0, daysOrMonths.Length);
            }
            choices[i] = daysOrMonths[rand_i];
        }
    }

    private bool contains(string[] choices, string v)
    {
        for (int i = 0; i < choices.Length; i++)
        {
            if (choices[i] == v) return true;
        }
        return false;
    }
}
