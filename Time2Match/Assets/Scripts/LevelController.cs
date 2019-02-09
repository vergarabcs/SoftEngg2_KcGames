using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    public Button botRightBtn;
    private DataController dc;
    public SceneInitializer si;
    public AnswerChecker answerChecker;


	// Use this for initialization
	void Start () {
        //InstantiatePrefabs();
        dc = GameObject.FindObjectOfType<DataController>();
        si = GameObject.FindObjectOfType<SceneInitializer>();
        answerChecker = new AnswerChecker(this, si);
	}

    public bool CheckAnswer()
    {
        bool isCorrect = answerChecker.CheckAnswer();
        if (dc.isPractice)
        {
            si.DisableInput();
            answerChecker.ShowMarks();
        }
        else
        {
            dc.DidAnswer(isCorrect);
        }
        return isCorrect;
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }
}
