using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour {
    public Text score;
    private DataController dc;
	// Use this for initialization
	void Start () {
        dc = FindObjectOfType<DataController>();
        score.text = "Score: " + dc.score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Restart()
    {
        dc.restart();
        SceneManager.LoadScene("MenuScreen");
    }
}
