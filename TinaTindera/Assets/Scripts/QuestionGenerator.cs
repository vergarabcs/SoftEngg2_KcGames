using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class QuestionGenerator{
    
    public Question[] GenerateQuestions()
    {
        Question[] qPool = new Question[10];
        int[] diffArr = generateRandomDifficulty();
        for(int i=0; i<qPool.Length; i++)
        {
            qPool[i] = GenerateQuestion(diffArr[i]);
        }
        return qPool;
    }

    private Question GenerateQuestion(int length)
    {
        PhraseGenerator pg = new PhraseGenerator();
        string question = null;
        string[] args = new string[length];
        int[] fruitCounts = new int[length];
        string[] fruitAnswers = new string[length];
        pg.storeToArr(args, fruitCounts, fruitAnswers, length);
        

        if (length == 1)
        {
            question = String.Format("Good Morning Miss Tina! I want to buy {0}", args);
        }
        else if (length == 2)
        {
            question = String.Format("Good Morning Miss Tina! I want to buy {0} and {1}", args);
        }else
        {
            question = String.Format("Good Morning Miss Tina! I want to buy {0}, {1}, and {2}", args);
        }
        return new Question(question, fruitCounts, fruitAnswers);
    }

    private int[] generateRandomDifficulty()
    {
        List<int> difficulties = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            difficulties.Add(1);
        }
        for (int i = 0; i < 3; i++)
        {
            difficulties.Add(2);
        }
        for (int i = 0; i < 2; i++)
        {
            difficulties.Add(3);
        }
        int[] diffArr = difficulties.ToArray();
        reshuffle<int>(diffArr);
        return diffArr;
    }

    public static void reshuffle<T>(T[] arr)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < arr.Length; t++)
        {
            T tmp = arr[t];
            int r = UnityEngine.Random.Range(t, arr.Length);
            arr[t] = arr[r];
            arr[r] = tmp;
        }
    }
}

internal class PhraseGenerator
{
    public static string[] FRUITS = {"Apple", "Banana", "Grape", "Lemon", "Orange", "Pineapple", "Strawberry"};

    public PhraseGenerator()
    {
        QuestionGenerator.reshuffle<string>(PhraseGenerator.FRUITS);
    }

    internal void storeToArr(string[] args, int[] fruitCounts, string[] fruitAnswers, int length)
    {
        for(int i=0; i<length; i++)
        {
            fruitCounts[i] = Random.Range(1, 11);

            //FRUITS IS already shuffled beforehand ""+ is so it will return a new string
            fruitAnswers[i] = ""+FRUITS[i];

            if (fruitCounts[i] > 1)
            {
                args[i] = fruitCounts[i].ToString() + " " + pluralize(fruitAnswers[i]);
            }
            else
            {
                args[i] = fruitCounts[i].ToString() + " " + fruitAnswers[i];
            }
        }
    }

    private string pluralize(string v)
    {
        if (v == "Strawberry")
        {
            return "Strawberries";
        }else if (v == "Grapes")
        {
            return "Grapes";
        }
        return v + "s";
    }
}