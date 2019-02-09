using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer:MonoBehaviour{
    Question currentQuestion;
    LevelController lc;
    public List<ChoiceController> choiceControllers = new List<ChoiceController>();
    public List<ClockController> clockControllers = new List<ClockController>();

    //CONSTANTS:
    private static float Y_RANGE = 3.1f; // vertically, clocks will be at -Y_RANGE to Y_RANGE
    private static float CHOICE_X = 4.0f;
    private static float CHOICE_Y_DECREMENT = 2.0f * Y_RANGE / 3.0f;
    private static float QUESTION_X = -4.0f;

    //Inspector inputs:
    public GameObject choicePrefab;
    public GameObject clockPrefab;

    private void Awake()
    {
        currentQuestion = GameObject.FindObjectOfType<DataController>().getCurrentQuestion();
        Init();
    }

    public SceneInitializer(Question currentQ)
    {
        this.currentQuestion = currentQ;
    }

    public void Init()
    {
        InstantiateClocks();
        InstantiateChoices();
    }

    internal void DisableInput()
    {
        GameObject.FindObjectOfType<DrawLine>().enabled = false;
    }

    private void InstantiateChoices()
    {
        float start = Y_RANGE;
        Utility.reshuffle<DateTime>(currentQuestion.clocks);
        if (currentQuestion.clocks.Length == 4)
        {
            for (int i = 0; i < currentQuestion.clocks.Length; i++)
            {
                DateTime time = currentQuestion.clocks[i];
                GameObject instance = UnityEngine.Object.Instantiate(choicePrefab, new Vector3(CHOICE_X, start), Quaternion.identity);
                ChoiceController cc = instance.GetComponent("ChoiceController") as ChoiceController;
                choiceControllers.Add(cc);
                cc.SetTime(time.Hour, time.Minute);
                start -= CHOICE_Y_DECREMENT;
            }
        }
        else
        {
            float decrement = (2 * Y_RANGE) / (currentQuestion.clocks.Length + 1);
            start = Y_RANGE - decrement;
            for (int i = 0; i < currentQuestion.clocks.Length; i++)
            {
                DateTime time = currentQuestion.clocks[i];
                GameObject instance = UnityEngine.Object.Instantiate(choicePrefab, new Vector3(CHOICE_X, start), Quaternion.identity);
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
        int analogClockCount = currentQuestion.clocks.Length - 1;
        if (currentQuestion.clocks.Length - 1 == 3)
        {

            for (int i = 0; i < analogClockCount; i++)
            {
                DateTime time = currentQuestion.clocks[i];
                GameObject instance = UnityEngine.Object.Instantiate(clockPrefab, new Vector3(QUESTION_X, start), Quaternion.identity);
                ClockController cc = instance.GetComponent("ClockController") as ClockController;
                clockControllers.Add(cc);
                cc.SetTime(time.Hour, time.Minute);
                start -= Y_RANGE;
            }
        }
        else
        {
            float decrement = (2 * Y_RANGE) / (analogClockCount + 1);
            start = Y_RANGE - decrement;
            if (analogClockCount == 2)
            {
                start = 2.0f;
                decrement = 2 * start;
            }
            for (int i = 0; i < analogClockCount; i++)
            {
                DateTime time = currentQuestion.clocks[i];
                GameObject instance = UnityEngine.Object.Instantiate(clockPrefab, new Vector3(QUESTION_X, start), Quaternion.identity);
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
        else if (clockControllers.Count == 2)
        {
            scaleMultiplier = 1.5f;
        }
        foreach (ClockController cc in clockControllers)
        {
            cc.transform.localScale *= scaleMultiplier;
            Transform rBtn = cc.transform.GetChild(2);
            rBtn.localScale /= scaleMultiplier;
            MonoBehaviour.print(rBtn.gameObject.name);
        }
    }
}
