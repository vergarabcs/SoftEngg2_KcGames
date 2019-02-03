using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonController : MonoBehaviour {
    DataController dc;
    private void Start()
    {
        dc = GameObject.FindObjectOfType<DataController>();
    }
    public void Home()
    {
        dc.restart();
        SceneManager.LoadScene("MenuScreen");
    }
}
