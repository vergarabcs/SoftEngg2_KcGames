using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public GameObject levelStarter;
    public GameObject scoreDisplay;
    private DataController dataController;
    private Question[] questionPool;

    private bool isActive;
    private int timeRemaining;
    private int questionIndex = 0;
    private int score = 0;

	// Use this for initialization
	void Start () {
        dataController = FindObjectOfType<DataController>();
        questionPool = dataController.getQuestions();
        StartLevel();
	}

    private void StartLevel()
    {
        GameObject lsInstance = Instantiate(levelStarter);
        RoundController rc = (RoundController)lsInstance.GetComponent("RoundController");
        rc.Initiate(questionPool[questionIndex], this);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void DidAnswer(bool isCorrect)
    {
        if(isCorrect) score++;
        questionIndex++;
        if (questionIndex < questionPool.Length)
        {
            StartLevel();
        }
        else
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        GameObject instance = Instantiate(scoreDisplay);
        Text t = (Text)instance.GetComponentInChildren<Text>();
        t.text = String.Format("Score: {0}", score);
        score = 0;
        questionIndex = 0;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menuscene");
    }
}
