using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour {
    public GameObject clock;
    public GameObject timeText;
    DataController dc;
    Question q;

    private void Start()
    {
        dc = GameObject.FindObjectOfType<DataController>();
        q = dc.getCurrentQuestion();
        //q = (new QuestionGenerator()).generateQuestion(4);
        
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
