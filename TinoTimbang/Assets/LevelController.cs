using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    private static float EASY_TIME = 60.0f;
    private static float MEDIUM_TIME = 40.0f;
    private static float DIFFICULT_TIME = 30.0f;
    private static float ACCEPTABLE_ERROR = 0.1f;


    Question q;
    List<float> leftMassTotal = new List<float>();
    float totalLeft = 0.0f;
    DataController dc;
    private Text timrText;

    private void Start()
    {
        dc = GameObject.FindObjectOfType<DataController>();
        //if (dc == null) SceneManager.LoadScene(0);
        //else
        {
            timrText = GameObject.Find("Timer").GetComponent<Text>();
            q = dc.getCurrentQuestion();
            InitiateLevel();
        }
        
    }

    private void InitiateLevel()
    {
        Vector2 spawnLocation = GameObject.Find("ContainerRight").transform.position;
        spawnLocation.Set(spawnLocation.x, spawnLocation.y - 1.0f);
        foreach (GameObject prefab in q.questions)
        {
            GameObject instance = Instantiate(prefab, spawnLocation, Quaternion.identity);
            instance.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            SpawnerScript ss = instance.GetComponent<SpawnerScript>();
            Destroy(ss);
            spawnLocation.Set(spawnLocation.x, spawnLocation.y + 2.0f);
        }
        StartTimer();
    }

    private void StartTimer()
    {
        TimerController tc = GameObject.FindObjectOfType<TimerController>();
        if(q.difficulty == Utility.EASY)
        {
            tc.SetTime(EASY_TIME);
        }else if(q.difficulty == Utility.MEDIUM)
        {
            tc.SetTime(MEDIUM_TIME);
        }
        else
        {
            tc.SetTime(DIFFICULT_TIME);
        }
        tc.StartTime();
    }

    public void removeFromLeft(float mass) { totalLeft -= mass; }
    
    public void addToLeft(float mass) { totalLeft += mass; }

    private bool isCorrect = false;
    public void checkAnswer()
    {
        DisableInput();
        SetTimeToZero();
        float totalRight = 0;
        foreach(GameObject g in q.questions)
        {
            totalRight += g.GetComponent<Rigidbody2D>().mass;
        }
        float error = Math.Abs(totalRight - totalLeft) / totalRight;
        if (error > ACCEPTABLE_ERROR)
        {
            isCorrect = false;
            //dc.DidAnswer(false);
        }
        else
        {
            isCorrect = true;
            //dc.DidAnswer(true);
        }
        Invoke("ShowFeedBack", 2.0f);
    }

    private void SetTimeToZero()
    {
        TimerController tc = GameObject.FindObjectOfType<TimerController>();
        tc.SetTime(0.0f);
    }

    private void ShowFeedBack()
    {
        if (isCorrect)
        {
            timrText.text = "Correct!";
        }
        else
        {
            timrText.text = "Wrong!";
        }
        Invoke("DcCaller", 2.0f);
    }

    void DcCaller()
    {
        dc.DidAnswer(isCorrect);
    }

    private void DisableInput()
    {
        SpawnerScript[] spawners = GameObject.FindObjectsOfType<SpawnerScript>();
        foreach(SpawnerScript ss in spawners)
        {
            Destroy(ss);
        }
    }
}
