using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    private static float Y_RANGE = 3.1f; // vertically, clocks will be at -Y_RANGE to Y_RANGE
    private static float CHOICE_X = 4.0f;
    private static float QUESTION_X = -4.0f;
    private static float CHOICE_Y_DECREMENT = 2.0f*Y_RANGE/3.0f;
    private static float FEEDBACK_DELAY = 0.5f;

    public Button botRightBtn;
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
        q = dc.getCurrentQuestion();
        //q = (new QuestionGenerator()).generateQuestion(4);
        InstantiatePrefabs();
	}

    void InstantiatePrefabs()
    {
        InstantiateClocks();
        InstantiateChoices();
    }

    private void InstantiateChoices()
    {
        float start = Y_RANGE;
        Utility.reshuffle<DateTime>(q.clocks);
        if (q.clocks.Length == 4)
        {
            for (int i = 0; i < q.clocks.Length; i++)
            {
                DateTime time = q.clocks[i];
                GameObject instance = Instantiate(choicePrefab, new Vector3(CHOICE_X, start), Quaternion.identity);
                ChoiceController cc = instance.GetComponent("ChoiceController") as ChoiceController;
                choiceControllers.Add(cc);
                cc.SetTime(time.Hour, time.Minute);
                start -= CHOICE_Y_DECREMENT;
            }
        }
        else
        {
            float decrement = (2 * Y_RANGE) / (q.clocks.Length + 1);
            start = Y_RANGE - decrement;
            for (int i = 0; i < q.clocks.Length; i++)
            {
                DateTime time = q.clocks[i];
                GameObject instance = Instantiate(choicePrefab, new Vector3(CHOICE_X, start), Quaternion.identity);
                ChoiceController cc = instance.GetComponent("ChoiceController") as ChoiceController;
                choiceControllers.Add(cc);
                cc.SetTime(time.Hour, time.Minute);
                start -= decrement;
            }
        }
        
    }

    private void InstantiateClocks()
    {
        float start = Y_RANGE;
        int analogClockCount = q.clocks.Length - 1;
        if (q.clocks.Length - 1 == 3)
        {
            
            for (int i = 0; i < analogClockCount ; i++)
            {
                DateTime time = q.clocks[i];
                GameObject instance = Instantiate(clockPrefab, new Vector3(QUESTION_X, start), Quaternion.identity);
                ClockController cc = instance.GetComponent("ClockController") as ClockController;
                clockControllers.Add(cc);
                cc.SetTime(time.Hour, time.Minute);
                start -= Y_RANGE;
            }
        }else
        {
            float decrement = (2*Y_RANGE) / (analogClockCount + 1);
            start = Y_RANGE - decrement;
            if (analogClockCount == 2)
            {
                start = 2.0f;
                decrement = 2*start;
            }
            for (int i = 0; i < analogClockCount; i++)
            {
                DateTime time = q.clocks[i];
                GameObject instance = Instantiate(clockPrefab, new Vector3(QUESTION_X, start), Quaternion.identity);
                ClockController cc = instance.GetComponent("ClockController") as ClockController;
                clockControllers.Add(cc);
                cc.SetTime(time.Hour, time.Minute);
                start -= decrement;
            }
        }
        ResizeClocks();
    }

    private void ResizeClocks()
    {
        float scaleMultiplier = 1.2f;
        if (clockControllers.Count == 1)
        {
            scaleMultiplier = 2.0f;
        }
        else if(clockControllers.Count==2)
        {
            scaleMultiplier = 1.5f;
        }
        foreach (ClockController cc in clockControllers)
        {
            cc.transform.localScale *= scaleMultiplier;
            Transform rBtn = cc.transform.GetChild(2);
            rBtn.localScale /= scaleMultiplier;
            print(rBtn.gameObject.name);
        }
    }

    public bool CheckAnswer()
    {
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
        if (isCorrect)
            CorrectAnimation();
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
    
    public void CorrectAnimation()
    {
        foreach(ClockController clockC in matchMade.Keys)
        {
            ChoiceController choiceC = matchMade[clockC];
            clockC.line.enabled = true;
            clockC.line.SetPosition(0, clockC.radioButton.position);
            clockC.line.SetPosition(1, choiceC.radioButton.position);
        }
        //answerFeedback.text = "CORRECT!";
        ShowMarks();
    }

    private void reenableButton()
    {
        botRightBtn.gameObject.SetActive(true);
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
