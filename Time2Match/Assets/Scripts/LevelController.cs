using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    private static float Y_RANGE = 3.0f; // vertically, clocks will be at -Y_RANGE to Y_RANGE
    private static float CHOICE_X = 4.0f;
    private static float QUESTION_X = -3.0f;
    private static float CHOICE_Y_DECREMENT = 2.0f*Y_RANGE/3.0f;
    private static float FEEDBACK_DELAY = 0.5f;

    public Button botRightBtn;
    public Text answerFeedback;
    Question q;
    public GameObject clockPrefab;
    public GameObject choicePrefab;
    private DataController dc;
    List<ClockController> clockControllers = new List<ClockController>();
    List<ChoiceController> choiceControllers = new List<ChoiceController>();
    Dictionary<ClockController, ChoiceController> matchMade = new Dictionary<ClockController, ChoiceController>();

	// Use this for initialization
	void Start () {
        dc = GameObject.FindObjectOfType<DataController>();
        //q = dc.getCurrentQuestion();
        q = (new QuestionGenerator()).generateQuestion(4);
        Test();
        if (dc.isInstruction)
        {
            ActivateInstruction();
        }
        answerFeedback.enabled = false;
	}

    private void ActivateInstruction()
    {
        
    }

    void Test()
    {
        float start = Y_RANGE;
        for(int i=0; i<q.clocks.Length-1; i++)
        {
            DateTime time = q.clocks[i];
            GameObject instance = Instantiate(clockPrefab, new Vector3(QUESTION_X, start), Quaternion.identity);
            ClockController cc = instance.GetComponent("ClockController") as ClockController;
            clockControllers.Add(cc);
            cc.SetTime(time.Hour, time.Minute);
            start -= Y_RANGE;
        }
        start = Y_RANGE;

        Utility.reshuffle<DateTime>(q.clocks);
        for(int i=0; i<q.clocks.Length; i++)
        {
            DateTime time = q.clocks[i];
            GameObject instance = Instantiate(choicePrefab, new Vector3(CHOICE_X, start), Quaternion.identity);
            ChoiceController cc = instance.GetComponent("ChoiceController") as ChoiceController;
            choiceControllers.Add(cc);
            cc.SetTime(time.Hour, time.Minute);
            start -= CHOICE_Y_DECREMENT;
        }
    }

    public bool CheckAnswer()
    {
        botRightBtn.gameObject.SetActive(false);
        if (matchMade.Count != clockControllers.Count)
        {
            ShowAnswer(false);
            return false;
        }
        foreach(ClockController cc1 in matchMade.Keys)
        {
            ChoiceController cc2 = matchMade[cc1];
            if(!(cc1.hour==cc2.hour && cc1.minute == cc2.minute))
            {
                ShowAnswer(false);
                return false;
            }
        }
        ShowAnswer(true);
        return true;
    }

    internal void unMatch(ClockController clockController)
    {
        this.matchMade.Remove(clockController);
    }

    private bool isCorrect = false;
    // this is for passing parameter to dcDidAnswerCaller
    private void ShowAnswer(bool isCorrect)
    {
        this.isCorrect = isCorrect;
        print("isCorrect: " + isCorrect.ToString());
        DisableInput();
        ShowMarks();
        StoreCorrectMappingToDict();
        //todo: show correct answer
        if (!isCorrect) WrongAnimation();
        else CorrectAnimation();
    }

    private void DisableInput()
    {
        GameObject.FindObjectOfType<DrawLine>().enabled = false;
    }

    private void StoreCorrectMappingToDict()
    {
        matchMade.Clear();
        foreach(ClockController clockC in clockControllers)
        {
            foreach(ChoiceController choiceC in choiceControllers)
            {
                if (clockC.hour == choiceC.hour && clockC.minute == choiceC.minute)
                    Match(choiceC, clockC);
            }
        }
    }
    
    private void CorrectAnimation()
    {
        foreach(ClockController clockC in matchMade.Keys)
        {
            ChoiceController choiceC = matchMade[clockC];
            clockC.line.enabled = true;
            clockC.line.SetPosition(0, clockC.radioButton.position);
            clockC.line.SetPosition(1, choiceC.radioButton.position);
        }
        answerFeedback.enabled = true;
        //answerFeedback.text = "CORRECT!";
        answerFeedback.text = "";
        ShowMarks();
        Invoke("reenableButton", FEEDBACK_DELAY);
    }

    private void reenableButton()
    {
        botRightBtn.gameObject.SetActive(true);
    }

    private void WrongAnimation()
    {
        
        answerFeedback.enabled = true;
        //answerFeedback.text = "WRONG!";
        answerFeedback.text = "";
        Invoke("CorrectAnimation", FEEDBACK_DELAY);
    }

    private void ShowMarks()
    {
        ClearMarks();
        foreach (ClockController clockC in clockControllers)
        {
            if (matchMade.ContainsKey(clockC))
            {
                ChoiceController choiceC = matchMade[clockC];
                bool isCorrect = clockC.hour == choiceC.hour && clockC.minute == choiceC.minute;
                choiceC.showMark(isCorrect);
                clockC.showMark(isCorrect);
            }
            else
            {
                clockC.showMark(false);
            }
        }
    }

    private void ClearMarks()
    {
        foreach (ClockController clockC in clockControllers)
        {
            clockC.ClearMark();
        }
        foreach(ChoiceController choiceC in choiceControllers)
        {
            choiceC.ClearMark();
        }
    }

    internal void Match(ChoiceController cc, ClockController clockController)
    {
        if (matchMade.ContainsKey(clockController))
        {
            matchMade[clockController] = cc;
        }
        else
        {
            matchMade.Add(clockController, cc);
        }
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }
}
