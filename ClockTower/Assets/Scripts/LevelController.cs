using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
    public GameObject feedBackTxt;
    public GameObject submitBtn;
    public GameObject clock;
    public GameObject timeText;
    Question q;

	// Use this for initialization
	void Start () {
        q = (new QuestionGenerator()).generateQuestion(2);

        InitiateLevel();
	}

    private void InitiateLevel()
    {
        SetTextTime(q.textHour, q.textMin);
        SetClockTime(q.clockHour, q.clockMin);
        ClockController clockC = clock.GetComponent<ClockController>();
        if (q.difficulty == 0)
        {
            clockC.DisableRotation(true);
        }else if(q.difficulty == 1)
        {

            clockC.DisableRotation(false);
        }
    }

    void SetTextTime(int hour, int minute)
    {
        TextMeshPro tMP = timeText.GetComponent<TextMeshPro>();
        tMP.text = Utility.timeToString(hour, minute);
    }

    void SetClockTime(int hour, int minute)
    {
        ClockController cc = clock.GetComponent<ClockController>();
        cc.SetTime(hour, minute);
    }
}
