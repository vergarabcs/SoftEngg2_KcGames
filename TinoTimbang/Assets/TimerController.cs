using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {
    LevelController lc;
    float time = 0;
    public Text txt;
    public bool isActive = false;
    private int frame = 0;

	// Use this for initialization
	void Start () {
        lc = GameObject.FindObjectOfType<LevelController>();
	}
	
    public void SetTime(float timeMs)
    {
        time = timeMs;
        if (txt != null)
        {
            txt.text = String.Format("{0:0.0}", time);
        }
    }

    public void StartTime()
    {
        isActive = true;
    }
    // Update is called once per frame

    private float elapsed = 0.0f;
	void Update () {
        elapsed += Time.deltaTime;
        if (isActive)
        {
            if (frame == 5)
            {
                this.SetTime(time - elapsed);
                frame = 0;
                elapsed = 0;
            }
            if (time <= 0)
            {
                isActive = false;
                this.SetTime(0.0f);
                lc.checkAnswer();
            }
            frame++;
        }
	}
}
