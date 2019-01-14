using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class DataController : MonoBehaviour {
    static private int CHOICE_COUNT = 3;
    public Question[] questionPool;
    public int questionIndex;
    public int score;

    static Dictionary<int, float> difficulty_diameter = new Dictionary<int, float>() {
        {0, 7.0f},
        {1, 6.0f},
        {2, 5.0f}
    };

    public void restart()
    {
        score = 0;
        questionPool = getQuestions();
        questionIndex = 0;
    }

    private void Start()
    {
        this.restart();
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Menuscreen");
    }

    public void DidAnswer(bool isCorrect)
    {
        if (isCorrect) score++;
        questionIndex++;
        if (questionIndex < questionPool.Length)
        {
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            SceneManager.LoadScene("EndGame");
        }
    }

    public Question[] getQuestions()
    {
        int[] difficulties = generateRandomDifficulty();
        Question[] questions = new Question[10];

        for (int i=0; i<difficulties.Length; i++)
        {
            Question q = GenerateQuestion(difficulties[i]);
            questions[i] = q;
        }
        return questions;
    }

    //0 easy, 1 med, 2 diff
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
        reshuffle<int>(diffArr);
        return diffArr;
    }

    //Generic array shuffle
    void reshuffle<T>(T[] arr)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < arr.Length; t++)
        {
            T tmp = arr[t];
            int r = Random.Range(t, arr.Length);
            arr[t] = arr[r];
            arr[r] = tmp;
        }
    }

    //generate question based from difficulty
    private Question GenerateQuestion(int difficulty)
    {
        float diameter = difficulty_diameter[difficulty];
        DateTime rndDate = genRndDate();
        string dayOrMonthOrYear = dayOrMonthOrYearFunc();
        string qText = String.Format("What {0} is it according to the calendar?", dayOrMonthOrYear);
        string answer = genAnswer(rndDate, dayOrMonthOrYear);
        string[] choices = genChoices(answer, rndDate, dayOrMonthOrYear);
        Question q = new Question(qText, answer, choices, diameter, rndDate);
        return q;
    }

    private string[] genChoices(string answer, DateTime rndDate, string d_M_Y)
    {
        HashSet<string> choices = new HashSet<string>();
        choices.Add(answer);
        for(int i=1; i<CHOICE_COUNT; i++)
        {
            DateTime rnDate = genRndDate();
            string choice = genAnswer(rnDate, d_M_Y);
            int infiniteCtr = 0;
            while (choices.Contains(choice))
            {
                 rnDate = genRndDate();
                 choice = genAnswer(rnDate, d_M_Y);
                infiniteCtr++;
                if (infiniteCtr > 500) break;
            }
            choices.Add(choice);
        }
        string[] temp = choices.ToArray();
        reshuffle<string>(temp);
        return temp;
    }

    private static string genAnswer(DateTime rndDate, string dayOrMonthOrYear)
    {
        if (dayOrMonthOrYear == "day")
        {
            return rndDate.DayOfWeek.ToString();
        }else if (dayOrMonthOrYear == "month")
        {
            return rndDate.Month.ToString();
        }
        else
        {
            return rndDate.Year.ToString();
        }
    }

    //return "day" or "month" or "year"
    private string dayOrMonthOrYearFunc()
    {
        int rn = Random.Range(0, 3);
        switch (rn)
        {
            case 0:
                return "day";
            case 1:
                return "month";
            default:
                return "year";
        }
    }

    private System.Random gen = new System.Random();
    

    private DateTime genRndDate()
    {
        DateTime start = new DateTime(2012, 1, 1);
        int range = (DateTime.Today-start).Days;
        return start.AddDays(gen.Next(range));
    }
}
