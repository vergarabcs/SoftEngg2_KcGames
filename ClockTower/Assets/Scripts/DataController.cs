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