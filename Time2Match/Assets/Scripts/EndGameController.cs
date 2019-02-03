using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour {
    public Text score;
    private DataController dc;
    public GameObject oneStar;
    public GameObject twoStars;
    public GameObject threeStars;

    // Use this for initialization
    void Start()
    {
        dc = FindObjectOfType<DataController>();
        if (dc.isPractice)
        {
            SceneManager.LoadScene("MenuScreen");
            return;
        }
        
        score.text = "Score: " + dc.score.ToString();

        if (dc.score >= 9)
        {
            threeStars.active = true;
        }else if(dc.score >= 5)
        {
            twoStars.active = true;
        }
        else
        {
            oneStar.active = true;
        }
    }

    public void Restart()
    {
        dc.restart();
        SceneManager.LoadScene("MenuScreen");
    }
}
