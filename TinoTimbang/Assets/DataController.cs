using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    private int questionIndex = 0;
    public int score = 0;
    private Question[] questionPool;
    private QuestionGenerator qc;
    public GameObject[] prefabs;

    public void restart()
    {
        score = 0;
        questionPool = qc.GenerateQuestions();
        questionIndex = 0;
    }

    // Use this for initialization
    private void Start()
    {
        qc = new QuestionGenerator();
        questionPool = qc.GenerateQuestions();
        this.restart();
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Menuscreen");
        //SceneManager.LoadScene("LevelScene");
        ScalePrefabs();
        Array.Sort(prefabs, Comparer); 
    }

    private static int Comparer(GameObject x, GameObject y)
    {
        float mass_x = x.GetComponent<Rigidbody2D>().mass;
        float mass_y = y.GetComponent<Rigidbody2D>().mass;
        if (mass_x > mass_y)
        {
            return 1;
        }else if(mass_x == mass_y)
        {
            return 0;
        }
        return -1;
    }

    private void ScalePrefabs()
    {
        foreach(GameObject gO in prefabs)
        {
            gO.transform.localScale = new Vector3(0.6f, 0.6f);
        }
    }

    internal Question getCurrentQuestion()
    {
        return questionPool[questionIndex];
    }

    public void DidAnswer(bool isCorrect)
    {
        print("didAnswer: " + isCorrect.ToString());
        if (isCorrect) score++;
        questionIndex++;
        if (questionIndex < questionPool.Length)
        {
            SceneManager.LoadScene("LevelScene");
        }
        else
        {
            SceneManager.LoadScene("EndGame");
        }
    }
}